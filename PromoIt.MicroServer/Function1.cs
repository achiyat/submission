//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System.Collections;
//using System.Text.Json;
//using static System.Collections.Specialized.BitVector32;
//using System.Net;
//using PromoIt.Entities;
//using PromoIt.Model;
//using System.Xml.Linq;
//using System.Diagnostics;
//using System.Security.Policy;
//using PromoIt.Entitis;
//using RestSharp;
//using Newtonsoft.Json.Linq;
//using Tweetinvi;
//using LinqToTwitter.OAuth;
//using LinqToTwitter;
//using System.Linq;

//namespace PromoIt.MicroServer
//{
//    public static class Function1
//    {
//        [FunctionName("Function1")]
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete", Route = "{User}/{page?}/{action}/{IdNumber?}/{IdNumber1?}")] HttpRequest req, string User, string page, string action, string IdNumber, string IdNumber1,
//            ILogger log)
//        {

//            Hashtable hash;
//            Activist Activ = new Activist();
//            Company company = new Company();
//            Association Asso = new Association();
//            Message message = new Message();
//            CampaignOfAsso campaignAsso = new CampaignOfAsso();
//            CampaignOfCompany campaignCompany = new CampaignOfCompany();
//            CampaignActivist campaignActivist = new CampaignActivist();
//            DonatedProduct product = new DonatedProduct();
//            InnerJoin innerJoin = new InnerJoin();
//            Shipping shipping = new Shipping();

//            string VarActivist = "@IDactivist,@NameActivist,@EmailActivist,@AddressActivist,@phoneActivist,@NameUserTweeter";
//            string VarCompany = "@IDCompany,@NameCompany,@OwnerCompany,@EmailCompany,@PhoneCompany";
//            string VarAssociation = "@IDassn,@NameAssn,@EmailAssn";

//            string VarCampaignAsso = $"@NameCampaign,{VarAssociation},@Fundraising,@linkURL,@Hashtag,@SelectedCampaign,@StatusCampaign";
//            string VarCampaignActivist = $"@IDcampaign,{VarCampaignAsso},{VarActivist},@MoneyActivist,@MoneySpent";
//            string VarCampaignCompany = $"@IDcampaign,{VarCampaignAsso},{VarCompany}";

//            string VarProduct = $"@ProductName,@Price,@Inventory,@SelectedProduct,@StatusProduct,{VarCampaignCompany}";
//            string VarShipping = $"@donated,@bought,@IDProduct,{VarProduct},{VarActivist},@MoneyActivist,@MoneySpent";

//            string responseMessage;
//            string requestBody;
//            string Query;

//            log.LogInformation("C# HTTP trigger function processed a request.");

//            switch (User)
//            {

//                case "Activist":
//                    switch (action)
//                    {
//                        case "AddActivist":
//                            try
//                            {
//                                //"Activist/AddActivist : insert new Activist"
//                                requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//                                Activ = System.Text.Json.JsonSerializer.Deserialize<Activist>(requestBody);
//                                Query = $"insert into Activists values({VarActivist})";
//                                MainManager.Instance.Activists.ExportFromDB(Query, Activ);
//                            }
//                            catch (Exception ex)
//                            {
//                                //$"Activist/Add : {ex.Message}"
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "chooseCampaigns":
//                            try
//                            {
//                                //**********************************/
//                                //"Activist/chooseCampaigns : insert new campaign Activist"
//                                requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//                                campaignActivist = System.Text.Json.JsonSerializer.Deserialize<CampaignActivist>(requestBody);
//                                Query = $"insert into campaignActivist values({VarCampaignActivist})";
//                                MainManager.Instance.CampaignActivists.ExportFromDB(Query, campaignActivist);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "AddShipments":
//                            try
//                            {
//                                //"Activist/AddShipments : insert new Shipments"
//                                shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(req.Body);
//                                Query = $"insert into Shipments values({VarShipping}) \r\n update DonatedProducts set SelectedProduct= @SelectedProduct where IDProduct = @IDProduct";
//                                MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "BuyProduct":
//                            try
//                            {
//                                //"Activist/BuyProduct : Updates the queries that the item was bought or donated"
//                                shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(req.Body);
//                                Query = "update Shipments set bought=@bought,donated=@donated where IDShipments = @IDShipments\r\n update Shipments set Inventory=@Inventory where IDProduct = @IDProduct\r\n update Shipments set Fundraising=@Fundraising where IDactivist = @IDactivist and IDcampaign = @IDcampaign \r\n update Shipments set MoneyActivist=@MoneyActivist,MoneySpent=@MoneySpent where IDactivist = @IDactivist\r\n update DonatedProducts set Inventory=@Inventory,Fundraising=@Fundraising where IDcampaign = @IDcampaign\r\n update campaignCompany set Fundraising=@Fundraising where IDcampaign = @IDcampaign\r\n update campaignAsso set Fundraising=@Fundraising where IDcampaign = @IDcampaign\r\n update campaignActivist set Fundraising=@Fundraising where IDactivist = @IDactivist and IDcampaign = @IDcampaign\r\n update campaignActivist set MoneyActivist=@MoneyActivist,MoneySpent=@MoneySpent where IDactivist = @IDactivist";
//                                MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "removeFromCrat":
//                            try
//                            {
//                                //"Activist/removeFromCrat : delete from Shipments"
//                                shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(req.Body);
//                                Query = "delete from Shipments where IDShipments = @IDShipments";
//                                MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "GetActivist":
//                            try
//                            {
//                                //"Activist/Get/null : Get Activists"
//                                hash = (Hashtable)MainManager.Instance.Activists.ImportData("select * from Activists");
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "GetActivistById":
//                            try
//                            {
//                                //"Activist/Get/IdNumber : Get Activist"
//                                hash = (Hashtable)MainManager.Instance.Activists.ImportData("select * from Activists");
//                                Activ = (Activist)hash[IdNumber]; //email
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(Activ);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                            // לבדוק האם אפשר להביא אובייט ריק וזה היחשב כאילו אין כלום
//                            //if (IdNumber == null) { }
//                            //else { }
//                    }
//                    break;

//                case "Company":
//                    switch (action)
//                    {
//                        case "AddCompany":
//                            try
//                            {
//                                //"Company/AddCompany : insert new Company"
//                                company = System.Text.Json.JsonSerializer.Deserialize<Company>(req.Body);
//                                Query = $"insert into companies values({VarCompany})";
//                                MainManager.Instance.Companies.ExportFromDB(Query, company);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "Upload":
//                            try
//                            {
//                                //"Company/Upload : insert new Product into DonatedProducts"
//                                requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//                                product = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>(requestBody);
//                                string SetVar = "NameCampaign=@NameCampaign,linkURL=@linkURL,Hashtag=@Hashtag,SelectedCampaign=@SelectedCampaign where IDcampaign = @IDcampaign";
//                                Query = $"insert into DonatedProducts values({VarProduct}) \r\n insert into campaignCompany values({VarCampaignCompany}) \r\n update campaignAsso set {SetVar} \r\n update campaignCompany set {SetVar} \r\n update DonatedProducts set {SetVar} \r\n update campaignActivist set {SetVar} \r\n update Shipments set {SetVar}";
//                                MainManager.Instance.DonatedProducts.ExportFromDB(Query, product);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "UpdateProduct":
//                            try
//                            {
//                                //"Company/UpdateProduct : Update Product "
//                                product = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>(req.Body);
//                                string SetVar = "ProductName=@ProductName,Price=@Price,Inventory=@Inventory,StatusProduct=@StatusProduct where IDProduct = @IDProduct";
//                                Query = $"update DonatedProducts set {SetVar} \r\n update Shipments set {SetVar}";
//                                MainManager.Instance.DonatedProducts.ExportFromDB(Query, product);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "DeleteProduct":
//                            try
//                            {
//                                //"Company/DeleteProduct : delete Product "
//                                product = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>(req.Body);
//                                Query = "delete from DonatedProducts where IDProduct = @IDProduct\r\n delete from Shipments where IDProduct = @IDProduct";
//                                MainManager.Instance.DonatedProducts.ExportFromDB(Query, product);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "GetCompany":
//                            try
//                            {
//                                //"Company/Get/null : Get Company"
//                                hash = (Hashtable)MainManager.Instance.Companies.ImportData("select * from companies");
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "GetCompanyById":
//                            try
//                            {
//                                //"Company/Get/IdNumber : Get CompanyById"
//                                hash = (Hashtable)MainManager.Instance.Companies.ImportData("select * from companies");
//                                company = (Company)hash[IdNumber]; //email
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(company);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                    }
//                    break;

//                case "Association":
//                    switch (action)
//                    {
//                        case "AddAssociation":
//                            try
//                            {
//                                //"Association/AddAssociation : insert new Association"
//                                Asso = System.Text.Json.JsonSerializer.Deserialize<Association>(req.Body);
//                                Query = $"insert into Associations values({VarAssociation})";
//                                MainManager.Instance.Associations.ExportFromDB(Query, Asso);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "Created":
//                            try
//                            {
//                                //"Association/Created : insert new campaign Association"
//                                campaignAsso = System.Text.Json.JsonSerializer.Deserialize<CampaignOfAsso>(req.Body);
//                                Query = $"insert into campaignAsso values({VarCampaignAsso})";
//                                MainManager.Instance.CampaignsAsso.ExportFromDB(Query, campaignAsso);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "UpdateCampaign":
//                            try
//                            {
//                                //"Association/UpdateCampaign : Update campaign Association"
//                                campaignAsso = System.Text.Json.JsonSerializer.Deserialize<CampaignOfAsso>(req.Body);
//                                string SetVar = "NameCampaign=@NameCampaign,linkURL=@linkURL,Hashtag=@Hashtag,SelectedCampaign=@SelectedCampaign,StatusCampaign=@StatusCampaign where IDcampaign = @IDcampaign";
//                                Query = $"update campaignAsso set {SetVar} \r\n update campaignCompany set {SetVar} \r\n update DonatedProducts set {SetVar} \r\n update campaignActivist set {SetVar} \r\n update Shipments set {SetVar}";
//                                MainManager.Instance.CampaignsAsso.ExportFromDB(Query, campaignAsso);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "DeleteCampaign":
//                            try
//                            {
//                                //"Association/DeleteCampaign : delete campaign Association"
//                                campaignAsso = System.Text.Json.JsonSerializer.Deserialize<CampaignOfAsso>(req.Body);
//                                Query = "delete from campaignAsso where IDcampaign = @IDcampaign";
//                                MainManager.Instance.CampaignsAsso.ExportFromDB(Query, campaignAsso);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "GetAssociation":
//                            try
//                            {
//                                //"Association/Get/null : Get Associations"
//                                hash = (Hashtable)MainManager.Instance.Associations.ImportData("select * from Associations");
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "GetAssociationById":
//                            try
//                            {
//                                //"Association/Get/null : Get Association"
//                                hash = (Hashtable)MainManager.Instance.Associations.ImportData("select * from Associations");
//                                Asso = (Association)hash[IdNumber]; //email
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(Asso);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;
//                    }
//                    break;

//                case "User":
//                    switch (action)
//                    {
//                        case "+100":
//                            try
//                            {
//                                shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(req.Body);
//                                Query = "update Shipments set MoneyActivist = @MoneyActivist where IDactivist = @IDactivist and IDcampaign = @IDcampaign \r\n update campaignActivist set MoneyActivist = @MoneyActivist where IDactivist = @IDactivist and IDcampaign = @IDcampaign";
//                                MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "message":
//                            try
//                            {
//                                Message data = new Message();
//                                data = System.Text.Json.JsonSerializer.Deserialize<Message>(req.Body);
//                                Query = "insert into messages values(@ID,@Name,@Phone,@Email,@Message)";
//                                MainManager.Instance.Messages.ExportFromDB(Query, data);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "Get-Campaigns":
//                            try
//                            {
//                                hash = (Hashtable)MainManager.Instance.CampaignsAsso.ImportData("select * from campaignAsso");
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "GetCampaignsById":
//                            try
//                            {
//                                hash = (Hashtable)MainManager.Instance.CampaignsAsso.ImportData("select * from campaignAsso where IDassn = " + IdNumber);
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "Get-Campaigns-Activist":
//                            try
//                            {
//                                hash = (Hashtable)MainManager.Instance.CampaignActivists.ImportData("select * from campaignActivist where IDactivist=" + IdNumber);
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "Get-Campaigns-Company":
//                            try
//                            {
//                                hash = (Hashtable)MainManager.Instance.CampaignCompany.ImportData("select * from campaignCompany where IDCompany=" + IdNumber);
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "SortCampaignsCompany":
//                            try
//                            {
//                                hash = (Hashtable)MainManager.Instance.CampaignCompany.ImportData("select * from campaignAsso c \r\n inner join campaignCompany ca\r\n on c.IDcampaign = ca.IDcampaign\r\nwhere ca.IDCompany =" + IdNumber);
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "Get-Product-Activist":
//                            try
//                            {
//                                hash = (Hashtable)MainManager.Instance.InnerJoins.ImportData("select * from DonatedProducts d inner join campaignActivist c on c.IDcampaign = d.IDcampaign where c.IDactivist=" + IdNumber + "and c.IDcampaign=" + IdNumber1 + "and d.StatusProduct=1 and d.StatusCampaign=1");
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "Get-Donated-products":
//                            try
//                            {
//                                hash = (Hashtable)MainManager.Instance.DonatedProducts.ImportData("select * from DonatedProducts");
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "Get-Shipments":
//                            try
//                            {
//                                hash = (Hashtable)MainManager.Instance.Shipments.ImportData("select * from Shipments");
//                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
//                                return new OkObjectResult(responseMessage);
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "Roles":
//                            try
//                            {
//                                var urlGetRoles = $"https://dev-1r64p6wfhjnlz8dm.us.auth0.com/api/v2/users/{IdNumber}/roles";
//                                var client = new RestClient(urlGetRoles);
//                                var request = new RestRequest("", Method.Get);
//                                string type = "Bearer ";
//                                string token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImpJZTZTbV9HTm9vci16OFgtTWJSSCJ9.eyJpc3MiOiJodHRwczovL2Rldi0xcjY0cDZ3Zmhqbmx6OGRtLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiI3TVBOSWl2anluaENibDYzZFByUnZOaElHMWtWZWdES0BjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly9kZXYtMXI2NHA2d2Zoam5sejhkbS51cy5hdXRoMC5jb20vYXBpL3YyLyIsImlhdCI6MTY3NjI4MjU5MywiZXhwIjoxNjc4ODc0NTkzLCJhenAiOiI3TVBOSWl2anluaENibDYzZFByUnZOaElHMWtWZWdESyIsInNjb3BlIjoicmVhZDpjbGllbnRfZ3JhbnRzIGNyZWF0ZTpjbGllbnRfZ3JhbnRzIGRlbGV0ZTpjbGllbnRfZ3JhbnRzIHVwZGF0ZTpjbGllbnRfZ3JhbnRzIHJlYWQ6dXNlcnMgdXBkYXRlOnVzZXJzIGRlbGV0ZTp1c2VycyBjcmVhdGU6dXNlcnMgcmVhZDp1c2Vyc19hcHBfbWV0YWRhdGEgdXBkYXRlOnVzZXJzX2FwcF9tZXRhZGF0YSBkZWxldGU6dXNlcnNfYXBwX21ldGFkYXRhIGNyZWF0ZTp1c2Vyc19hcHBfbWV0YWRhdGEgcmVhZDp1c2VyX2N1c3RvbV9ibG9ja3MgY3JlYXRlOnVzZXJfY3VzdG9tX2Jsb2NrcyBkZWxldGU6dXNlcl9jdXN0b21fYmxvY2tzIGNyZWF0ZTp1c2VyX3RpY2tldHMgcmVhZDpjbGllbnRzIHVwZGF0ZTpjbGllbnRzIGRlbGV0ZTpjbGllbnRzIGNyZWF0ZTpjbGllbnRzIHJlYWQ6Y2xpZW50X2tleXMgdXBkYXRlOmNsaWVudF9rZXlzIGRlbGV0ZTpjbGllbnRfa2V5cyBjcmVhdGU6Y2xpZW50X2tleXMgcmVhZDpjb25uZWN0aW9ucyB1cGRhdGU6Y29ubmVjdGlvbnMgZGVsZXRlOmNvbm5lY3Rpb25zIGNyZWF0ZTpjb25uZWN0aW9ucyByZWFkOnJlc291cmNlX3NlcnZlcnMgdXBkYXRlOnJlc291cmNlX3NlcnZlcnMgZGVsZXRlOnJlc291cmNlX3NlcnZlcnMgY3JlYXRlOnJlc291cmNlX3NlcnZlcnMgcmVhZDpkZXZpY2VfY3JlZGVudGlhbHMgdXBkYXRlOmRldmljZV9jcmVkZW50aWFscyBkZWxldGU6ZGV2aWNlX2NyZWRlbnRpYWxzIGNyZWF0ZTpkZXZpY2VfY3JlZGVudGlhbHMgcmVhZDpydWxlcyB1cGRhdGU6cnVsZXMgZGVsZXRlOnJ1bGVzIGNyZWF0ZTpydWxlcyByZWFkOnJ1bGVzX2NvbmZpZ3MgdXBkYXRlOnJ1bGVzX2NvbmZpZ3MgZGVsZXRlOnJ1bGVzX2NvbmZpZ3MgcmVhZDpob29rcyB1cGRhdGU6aG9va3MgZGVsZXRlOmhvb2tzIGNyZWF0ZTpob29rcyByZWFkOmFjdGlvbnMgdXBkYXRlOmFjdGlvbnMgZGVsZXRlOmFjdGlvbnMgY3JlYXRlOmFjdGlvbnMgcmVhZDplbWFpbF9wcm92aWRlciB1cGRhdGU6ZW1haWxfcHJvdmlkZXIgZGVsZXRlOmVtYWlsX3Byb3ZpZGVyIGNyZWF0ZTplbWFpbF9wcm92aWRlciBibGFja2xpc3Q6dG9rZW5zIHJlYWQ6c3RhdHMgcmVhZDppbnNpZ2h0cyByZWFkOnRlbmFudF9zZXR0aW5ncyB1cGRhdGU6dGVuYW50X3NldHRpbmdzIHJlYWQ6bG9ncyByZWFkOmxvZ3NfdXNlcnMgcmVhZDpzaGllbGRzIGNyZWF0ZTpzaGllbGRzIHVwZGF0ZTpzaGllbGRzIGRlbGV0ZTpzaGllbGRzIHJlYWQ6YW5vbWFseV9ibG9ja3MgZGVsZXRlOmFub21hbHlfYmxvY2tzIHVwZGF0ZTp0cmlnZ2VycyByZWFkOnRyaWdnZXJzIHJlYWQ6Z3JhbnRzIGRlbGV0ZTpncmFudHMgcmVhZDpndWFyZGlhbl9mYWN0b3JzIHVwZGF0ZTpndWFyZGlhbl9mYWN0b3JzIHJlYWQ6Z3VhcmRpYW5fZW5yb2xsbWVudHMgZGVsZXRlOmd1YXJkaWFuX2Vucm9sbG1lbnRzIGNyZWF0ZTpndWFyZGlhbl9lbnJvbGxtZW50X3RpY2tldHMgcmVhZDp1c2VyX2lkcF90b2tlbnMgY3JlYXRlOnBhc3N3b3Jkc19jaGVja2luZ19qb2IgZGVsZXRlOnBhc3N3b3Jkc19jaGVja2luZ19qb2IgcmVhZDpjdXN0b21fZG9tYWlucyBkZWxldGU6Y3VzdG9tX2RvbWFpbnMgY3JlYXRlOmN1c3RvbV9kb21haW5zIHVwZGF0ZTpjdXN0b21fZG9tYWlucyByZWFkOmVtYWlsX3RlbXBsYXRlcyBjcmVhdGU6ZW1haWxfdGVtcGxhdGVzIHVwZGF0ZTplbWFpbF90ZW1wbGF0ZXMgcmVhZDptZmFfcG9saWNpZXMgdXBkYXRlOm1mYV9wb2xpY2llcyByZWFkOnJvbGVzIGNyZWF0ZTpyb2xlcyBkZWxldGU6cm9sZXMgdXBkYXRlOnJvbGVzIHJlYWQ6cHJvbXB0cyB1cGRhdGU6cHJvbXB0cyByZWFkOmJyYW5kaW5nIHVwZGF0ZTpicmFuZGluZyBkZWxldGU6YnJhbmRpbmcgcmVhZDpsb2dfc3RyZWFtcyBjcmVhdGU6bG9nX3N0cmVhbXMgZGVsZXRlOmxvZ19zdHJlYW1zIHVwZGF0ZTpsb2dfc3RyZWFtcyBjcmVhdGU6c2lnbmluZ19rZXlzIHJlYWQ6c2lnbmluZ19rZXlzIHVwZGF0ZTpzaWduaW5nX2tleXMgcmVhZDpsaW1pdHMgdXBkYXRlOmxpbWl0cyBjcmVhdGU6cm9sZV9tZW1iZXJzIHJlYWQ6cm9sZV9tZW1iZXJzIGRlbGV0ZTpyb2xlX21lbWJlcnMgcmVhZDplbnRpdGxlbWVudHMgcmVhZDphdHRhY2tfcHJvdGVjdGlvbiB1cGRhdGU6YXR0YWNrX3Byb3RlY3Rpb24gcmVhZDpvcmdhbml6YXRpb25zIHVwZGF0ZTpvcmdhbml6YXRpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25zIGRlbGV0ZTpvcmdhbml6YXRpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25fbWVtYmVycyByZWFkOm9yZ2FuaXphdGlvbl9tZW1iZXJzIGRlbGV0ZTpvcmdhbml6YXRpb25fbWVtYmVycyBjcmVhdGU6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIHJlYWQ6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIHVwZGF0ZTpvcmdhbml6YXRpb25fY29ubmVjdGlvbnMgZGVsZXRlOm9yZ2FuaXphdGlvbl9jb25uZWN0aW9ucyBjcmVhdGU6b3JnYW5pemF0aW9uX21lbWJlcl9yb2xlcyByZWFkOm9yZ2FuaXphdGlvbl9tZW1iZXJfcm9sZXMgZGVsZXRlOm9yZ2FuaXphdGlvbl9tZW1iZXJfcm9sZXMgY3JlYXRlOm9yZ2FuaXphdGlvbl9pbnZpdGF0aW9ucyByZWFkOm9yZ2FuaXphdGlvbl9pbnZpdGF0aW9ucyBkZWxldGU6b3JnYW5pemF0aW9uX2ludml0YXRpb25zIHJlYWQ6b3JnYW5pemF0aW9uc19zdW1tYXJ5IGNyZWF0ZTphY3Rpb25zX2xvZ19zZXNzaW9ucyBjcmVhdGU6YXV0aGVudGljYXRpb25fbWV0aG9kcyByZWFkOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMgdXBkYXRlOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMgZGVsZXRlOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.KIGtt7sUaLO90cBsRwefCLCrZOrO2iqIPAMHkFHb3x9ubULspx5gMV19D-7ExaxvNa0-2pA7ex3ixIJtdOeWStqqnGnpuS9rKNu0uq4QqtlC_WDLQaaM3qYQLKRwFBoE_Xkho_3t-9-qDOeKpBV6MTlHPMu9ZgPKsvs2JKo_nOcTDCxkr1S-FMb0ms8wBqWeKwMqgLBdMH29dvk-FwyV7lF5WQ_QitRQnva2TAYEl6GL2NYRtX1PDuPDvWeXM7-3I6PjQCIvGuPIwYi47cu4Iip4BYATEYTcSdOsl6p5Q9XhDuRi0HetDwVYq3LQHBJE2QH6xMRjkElHMgpwPN-q_A";

//                                request.AddHeader("authorization", type + token);

//                                var response = client.Execute(request);
//                                if (response.IsSuccessful)
//                                {
//                                    var json = JArray.Parse(response.Content);
//                                    return new OkObjectResult(json);
//                                }
//                                else
//                                {
//                                    return new NotFoundResult();
//                                }
//                            }
//                            catch (Exception ex)
//                            {
//                                Console.WriteLine(ex.Message);
//                            }
//                            break;

//                        case "Twitter":
//                            //MainManager.Instance.logger.LogEvent("Twitter");
//                            string ConsumerKey = "TgYbOg6MYXxYEamj7giAfh37b";
//                            string ConsumerSecret = "l2OK0LPIxb9Op6nZBeKIgL56eJyvxFEMRxLj5NhOzykRG8IgEH";
//                            string AccessToken = "1617200909900980224-P5IEw1WJ0kIF9JLHpNX5jVNBnymTRM";
//                            string AccessTokenSecret = "ZwQ8BEleGPBUgG5BppxWo2bY5hgSvS6MIAL43fLFhxRVq";


//                            var userClient = new TwitterClient(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);

//                            var user = await userClient.Users.GetAuthenticatedUserAsync();
//                            Console.WriteLine(user);

//                            var tweet = await userClient.Tweets.PublishTweetAsync("#Hello #tweetinvi #world!");
//                            Console.WriteLine("You published the tweet : " + tweet);

//                            break;
//                    }
//                    break;

//                default:
//                    return new BadRequestObjectResult("Failed Request");
//                    break;
//            }

//            return null;
//        }
//    }

//}

