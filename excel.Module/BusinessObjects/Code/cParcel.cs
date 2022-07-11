// Class Name : cParcel.cs 
// Project Name : EXELS 
// Last Update : 4/19/2022 3:15:49 PM  
 
// Gen Code Version : 1.0.0.0  
 
// Revise ==> Revice date : 4/19/2022 3:15:49 PM 
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
   [DefaultProperty("")]
   [NavigationItem("Track Parcel")]
   // Standard Document
   [System.ComponentModel.DisplayName("Track Parcel")]
   public class cParcel : XPObject
   {
     public cParcel(Session session) : base(session) 
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
     } 
     protected override void OnSaving()
     {
            SendEmail();
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

        
        public void SendEmail()
        {
            ASEmailService oSendEmail;
            string emailFrom;
            string emailto;
            string emailcc;
            string emailSubject;
            string emailbcc;
            string emailBody;

            emailFrom = "sysexels@gmail.com";
            emailto = $" sysexels@gmail.com ; {recipient_email} ; {sender_email} ";
            emailSubject = $" EXELS PARCEL - {this.delivery_number.delivery_number}";
            emailBody = $" Sender Name: {this.sender_id.customer_name} {System.Environment.NewLine} Sender Address: {this.sender_address} {System.Environment.NewLine} " +
                $"Recipient Name: {this.recipient_id.customer_name} {System.Environment.NewLine} Recipient Address: {this.recipient_address} {System.Environment.NewLine}" +
                $"Delivery Date: {this.delivery_date} {System.Environment.NewLine} Status Delivery: {this.statustp.ToString()} {System.Environment.NewLine} Status Date: {this.arrival_date} {System.Environment.NewLine}  Notes:  {this.notes}";
            oSendEmail = new ASEmailService();
           // oSendEmail.

            oSendEmail.SendEmail(emailFrom, emailto, "", emailSubject, "", emailBody, "");


        }
     [Appearance("VisiblecParcelOID", Visibility = ViewItemVisibility.Hide)] 
     public int Oid 
     { 
         get { return base.Oid; }
         set { base.Oid = value; }
     }
        // 
        // Notes for cParcel : 
        private cDelivery _delivery_number;
        [XafDisplayName("Delivery Number"), ToolTip("Delivery Number")]
        // [Appearance("cDeliverydelivery_date", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        [ImmediatePostData]
        public cDelivery delivery_number
        {
            get { return _delivery_number; }
            set
            {
                SetPropertyValue(nameof(delivery_number), ref _delivery_number, value);
                if (!IsSaving && !IsLoading && value != null)
                {
                    delivery_date = value.delivery_date;
                    sender_id = value.sender_id;
                    sender_address = value.sender_address;
                    recipient_id = value.recipient_id;
                    recipient_address = value.recipient_address;
                    id_driver = value.id_driver;
                    truck_number = value.truck_number;
                }
            }
        }

        private DateTime _delivery_date;
        [XafDisplayName("Delivery Date"), ToolTip("Parcel Delivery Date")]
        [Appearance("cParceldelivery_date", Enabled = false)]
        // [Appearance("cDeliverydelivery_date", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public DateTime delivery_date
        {
            get { return _delivery_date; }
            set { SetPropertyValue(nameof(delivery_date), ref _delivery_date, value); }
        }

        private cCustomer _sender_id;
        [XafDisplayName("Sender Name"), ToolTip("Sender Name")]
        // [Appearance("cOrdersender_id", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public cCustomer sender_id
        {
            get { return _sender_id; }
            set
            {
                SetPropertyValue(nameof(sender_id), ref _sender_id, value);
                if (!IsSaving && !IsLoading && value != null)
                {
                    sender_address = value.customer_address;
                    sender_email = value.customer_email;
                }
            }
        }

        private String _sender_address;
        [XafDisplayName("Sender Address"), ToolTip("Sender Address")]
        [Appearance("cParcelsender_address", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String sender_address
        {
            get { return _sender_address; }
            set { SetPropertyValue(nameof(sender_address), ref _sender_address, value); }
        }

        private String _sender_email;
        [XafDisplayName("Sender Email"), ToolTip("Sender Email")]
        [Appearance("cParcelsender_email", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String sender_email
        {
            get { return _sender_email; }
            set { SetPropertyValue(nameof(sender_email), ref _sender_email, value); }
        }

        private cCustomer _recipient_id;
        [XafDisplayName("Recipient Name"), ToolTip("Recipient Name")]
        // [Appearance("cOrderrecipient_id", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public cCustomer recipient_id
        {
            get { return _recipient_id; }
            set
            {
                SetPropertyValue(nameof(recipient_id), ref _recipient_id, value);
                if (!IsSaving && !IsLoading && value != null)
                {
                    recipient_address = value.customer_address;
                    recipient_email = value.customer_email;
                }
            }
        }

        private String _recipient_address;
        [XafDisplayName("Recipient Address"), ToolTip("Recipient Address")]
        [Appearance("cOrderrecipient_address", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String recipient_address
        {
            get { return _recipient_address; }
            set { SetPropertyValue(nameof(recipient_address), ref _recipient_address, value); }
        }

        private String _recipient_email;
        [XafDisplayName("Recipient Email"), ToolTip("Recipient Email")]
        [Appearance("cOrderrecipient_email", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String recipient_email
        {
            get { return _recipient_email; }
            set { SetPropertyValue(nameof(recipient_email), ref _recipient_email, value); }
        }
        // 
        // Notes for cParcel : 
        private cDriver _id_driver;
        [XafDisplayName("Driver Name"), ToolTip("Driver Name")]
        // [Appearance("cOrderrecipient_id", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public cDriver id_driver
        {
            get { return _id_driver; }
            set
            {
                SetPropertyValue(nameof(id_driver), ref _id_driver, value);
                if (!IsSaving && !IsLoading && value != null)
                {
                    truck_number = value.truck_number;
                }
            }
        }

        private string _truck_number;
        [XafDisplayName("Truck Plates"), ToolTip("Truck Plates")]
        [Appearance("cDrivertruck_number", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        [Size(50)]
        public string truck_number
        {
            get { return _truck_number; }
            set { SetPropertyValue(nameof(truck_number), ref _truck_number, value); }
        }
        // 
        // Notes for cParcel : 
     private eStatusTP _status_tp; 
     [XafDisplayName("Status (TP)"), ToolTip("Status Track Parcel")] 
     // [Appearance("cParcelstatus_tp", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(50)] 
     public  eStatusTP statustp
     { 
       get { return _status_tp; } 
       set { SetPropertyValue(nameof(statustp), ref _status_tp, value); } 
     } 
     // 
     // Notes for cParcel : 
     private DateTime _arrival_date; 
     [XafDisplayName("Status Date"), ToolTip("Parcel Status Date")] 
     // [Appearance("cParcelarrival_date", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     public  DateTime arrival_date
     { 
       get { return _arrival_date; } 
       set { SetPropertyValue(nameof(arrival_date), ref _arrival_date, value); } 
     } 
     // 
     // Notes for cParcel : 
     private string _notes; 
     [XafDisplayName("Notes"), ToolTip("Notes About Parcel")] 
     // [Appearance("cParcelnotes", Enabled = true)]
     // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
     // [RuleRequiredField(DefaultContexts.Save)] 
     // [IsSearch(true)]
     [Size(100)] 
     public  string notes
     { 
       get { return _notes; } 
       set { SetPropertyValue(nameof(notes), ref _notes, value); } 
     }

        public enum eStatusTP
        {
            [XafDisplayName("Yet to be delivered")]
            Yet_to_be_Delivered = 0,
            [XafDisplayName("On The Way")]
            On_The_Way = 1,
            [XafDisplayName("Arrived at the destination")]
            Arrived_at_the_Destination = 2,
            [XafDisplayName("Received by the recipient")]
            Received_by_the_Recipient = 3
        }
    } 
} 
