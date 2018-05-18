using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelShopModel;
using JewelShopService.BindingModels;
using JewelShopService.Interfaces;
using JewelShopService.ViewModels;
using System.Data.Entity.SqlServer;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace JewelShopService.ImplementationsBD
{
  public  class MainServiceBD:IMainService
    {
        private AbstractDataBaseContext context;

        public MainServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }

        public List<ProdOrderViewModel> GetList()
        {
            List<ProdOrderViewModel> result = context.ProdOrders
                .Select(rec => new ProdOrderViewModel
                {
                    id = rec.id,
                    buyerId = rec.buyerId,
                    adornmentId = rec.adornmentId,
                    customerId = rec.customerId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    DateCustom = rec.DateCreate == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateCustom.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateCustom.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateCustom.Value),
                    status = rec.status.ToString(),
                    count = rec.count,
                    sum = rec.sum,
                    buyerFIO = rec.Buyer.buyerFIO,
                    adornmentName = rec.Adornment.adornmentName,
                    customerName = rec.Customer.customerFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrder(ProdOrderBindingModel model)
        {
            var order = new ProdOrder
            {
                buyerId = model.buyerId,
                adornmentId = model.adornmentId,
                DateCreate = DateTime.Now,
                count = model.count,
                sum = model.sum,
                status = ProdOrderStatus.Принят
            };
            context.ProdOrders.Add(order);
            context.SaveChanges();

            var client = context.Buyers.FirstOrDefault(x => x.id == model.buyerId);
            SendEmail(client.mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} создан успешно", order.id,
                order.DateCreate.ToShortDateString()));
        }

        public void TakeOrderInWork(ProdOrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    ProdOrder element = context.ProdOrders
                        .FirstOrDefault(rec => rec.id == model.id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var productComponents = context.AdornmentElements
                                                .Where(rec => rec.adornmentId == element.adornmentId);
                    foreach (var productComponent in productComponents)
                    {
                        int countOnStocks = productComponent.count * element.count;
                        var stockComponents = context.HangarElements
                                                    .Where(rec => rec.elementtId == productComponent.elementId);
                        foreach (var stockComponent in stockComponents)
                        {
                            if (stockComponent.count >= countOnStocks)
                            {
                                stockComponent.count -= countOnStocks;
                                countOnStocks = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStocks -= stockComponent.count;
                                stockComponent.count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStocks > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                productComponent.Element.elementName + " требуется " +
                                productComponent.count + ", не хватает " + countOnStocks);
                        }
                    }
                    element.customerId = model.customerId;
                    element.DateCustom = DateTime.Now;
                    element.status = ProdOrderStatus.Выполняетя;
                    context.SaveChanges();
                    SendEmail(element.Buyer.mail, "Оповещение по заказам",
                        string.Format("Заказ №{0} от {1} передеан в работу", element.id, element.DateCreate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishOrder(int id)
        {
            ProdOrder element = context.ProdOrders
                .FirstOrDefault(rec => rec.id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.status = ProdOrderStatus.Готов;
            context.SaveChanges();
            SendEmail(element.Buyer.mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} передан на оплату", element.id,
                element.DateCreate.ToShortDateString()));
        }

        public void PayOrder(int id)
        {
            ProdOrder element = context.ProdOrders
                .FirstOrDefault(rec => rec.id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.status = ProdOrderStatus.Оплачен;
            context.SaveChanges();
            SendEmail(element.Buyer.mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} оплачен успешно", element.id, element.DateCreate.ToShortDateString()));
        }

        public void PutComponentOnStock(HangarElementBindingModel model)
        {
            HangarElement element = context.HangarElements
                                                  .FirstOrDefault(rec => rec.hangarId == model.hangarId &&
                                                                      rec.elementtId == model.elementId);
            if (element != null)
            {
                element.count += model.Count;
            }
            else
            {
                context.HangarElements.Add(new HangarElement
                {
                    hangarId = model.hangarId,
                    elementtId = model.elementId,
                    count = model.Count
                });
            }
            context.SaveChanges();
        }

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;

            context.MessageInfos.Add(new MessageInfo
            {
                MessageId = mailAddress,
                FromMailAddress = ConfigurationManager.AppSettings["MailLogin"],
                Subject = subject,
                Body = text,
                DateDelivery = DateTime.Now,
                buyerId = null
            });
            context.SaveChanges();

            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]);

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}
