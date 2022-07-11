// Class Name : cShippingDocument.cs 
// Project Name : EXELS 
// Last Update : 4/19/2022 3:15:32 PM  
 
// Gen Code Version : 1.0.0.0  
 
// Revise ==> Revice date : 4/19/2022 3:15:32 PM 
 // Updated :   
//======================================================================== 
 
using System; 
using DevExpress.Xpo; 
using DevExpress.Persistent.Base; 
using System.ComponentModel; 
using DevExpress.Persistent.Validation; 
using DevExpress.ExpressApp.DC; 
using DevExpress.ExpressApp.ConditionalAppearance; 
using DevExpress.ExpressApp; 
using DevExpress.Data.Filtering; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;

namespace exels.Module.BusinessObjects 
{ 
   [DefaultClassOptions] 
   [ImageName("ModelEditor_Views")] 
   [DefaultProperty("document_name")]
   [NavigationItem("ShippingDocument")]
   // Standard Document
   [System.ComponentModel.DisplayName("Shipping Document")]
   public class cShippingDocument : XPObject
   {
     public cShippingDocument(Session session) : base(session) 
     {
       // This constructor is used when an object is loaded from a persistent storage.
       // Do not place any code here.
     }
     public override void AfterConstruction()
     {
       base.AfterConstruction();
       // Place here your initialization code.
       //SecuritySystem.CurrentUserName
       //LastUpdateUser = Session.GetObjectByKey<GPUser>(SecuritySystem.CurrentUserId);
       string tUser = SecuritySystem.CurrentUserName.ToString();
            //LastUpdateUser = Session.FindObject<GPUser>(new BinaryOperator("UserName", SecuritySystem.CurrentUserName.ToString())); 
            // LastUpdateUser = Session.FindObject<GPUser>(new BinaryOperator("UserName", tUser)); 
            // LastUpdate = DateTime.Now; 


            ASEmailService oSendEmail ;
            string emailFrom;
            string emailto;
            string emailcc;
            string emailSubject;
            string emailbcc;
            string emailBody;

            emailFrom = "annisaedrianf@gmail.com";
            emailto = "annisaedrianf@gmail.com;srieddy.suasono@genting.com";
            emailSubject = " tese email";
            emailBody = "test email";
            oSendEmail = new ASEmailService();
            oSendEmail.SendEmail(emailFrom, emailto, "", emailSubject,"", emailBody,"");
     } 
     protected override void OnSaving()
     {
       base.OnSaving();
     } 
     protected override void OnSaved()
     {
       base.OnSaved();
     } 
     protected override void OnDeleting()
     {
       base.OnDeleting();
     } 
     protected override void OnDeleted()
     {
       base.OnDeleted();
     } 
     public void Sync()
     {
     } 
     [Appearance("VisiblecShippingDocumentOID", Visibility = ViewItemVisibility.Hide)] 
     public int Oid 
     { 
         get { return base.Oid; }
         set { base.Oid = value; }
     }
     // 
     // Notes for cShippingDocument : 
     private cOrder _delivery_number; 
     [XafDisplayName("delivery number"), ToolTip("delivery number")] 
     // [Appearance("cShippingDocumentdelivery_number", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     public  cOrder delivery_number
     { 
       get { return _delivery_number; } 
       set { SetPropertyValue(nameof(delivery_number), ref _delivery_number, value); } 
     } 
     // 
     // Notes for cShippingDocument : 
     private string _document_name; 
     [XafDisplayName("Urutan produk"), ToolTip("Urutan produk")] 
     // [Appearance("cShippingDocumentdocument_name", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(255)] 
     public  string document_name
     { 
       get { return _document_name; } 
       set { SetPropertyValue(nameof(document_name), ref _document_name, value); } 
     } 
     // 
     // Notes for cShippingDocument : 
     private DateTime _date; 
     [XafDisplayName("Urutan produk"), ToolTip("Urutan produk")] 
     // [Appearance("cShippingDocumentdate", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     public  DateTime date
     { 
       get { return _date; } 
       set { SetPropertyValue(nameof(date), ref _date, value); } 
     } 
   } 
} 
