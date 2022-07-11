// Class Name : cOrder.cs 
// Project Name : EXELS 
// Last Update : 4/19/2022 3:15:08 PM  
 
// Gen Code Version : 1.0.0.0  
 
// Revise ==> Revice date : 4/19/2022 3:15:08 PM 
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
    [DefaultProperty("order_number")]
    [NavigationItem("Shipment")]
    // Standard Document
    [System.ComponentModel.DisplayName("Order")]
    public class cOrder : XPObject
    {
        public cOrder(Session session) : base(session)
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
            DateTime sDate = DateTime.Now;
            order_number = Number(sDate);
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
        [Appearance("VisiblecOrderOID", Visibility = ViewItemVisibility.Hide)]
        public int Oid
        {
            get { return base.Oid; }
            set { base.Oid = value; }
        }
        // 
        // Notes for cOrder : 
        private String _order_number;
        [XafDisplayName("Order Number"), ToolTip("Order Number")]
        [Appearance("cOrderdelivery_number", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String order_number
        {
            get { return _order_number; }
            set { SetPropertyValue(nameof(order_number), ref _order_number, value); }
        }

        public virtual string Number(DateTime sDate)
        {
            
            string sNumer = "";
            int sRun = 1;
            byte iYear = (byte)((sDate.Year - 2022));
            string sYear = $"OND.{sDate.ToString("yyMM")}";
            XPCollection<cOrder> xpDM = new XPCollection<cOrder>(Session);
            string sNumberMax = "";
            try
            {
                sNumberMax = xpDM
               //SelectMany(c => c.).
               .Where(o => o.order_number.Substring(0, 8).ToUpper().Trim() == sYear.ToUpper().Trim()
                    && o.order_number.Substring(9, 4).All(c => (c >= 48 && c <= 57))
                     )
               .Max(o => o.order_number.Trim());
            }
            catch (Exception e)
            {
                sNumberMax = "";
            }
            if (sNumberMax != null)
            {
                if (sNumberMax.Length == 13)
                {
                    sNumberMax = sNumberMax.Substring(9, 4);
                    sRun = System.Convert.ToInt32(sNumberMax) + 1;
                }
            }

            sNumer = $"{sYear}.{sRun.ToString("0000")}";

            return sNumer;
        }

        // 
        // Notes for cOrder : 
        private DateTime _order_date;
        [XafDisplayName("Order Date"), ToolTip("Order Date")]
        // [Appearance("cOrderorder_date", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public DateTime order_date
        {
            get { return _order_date; }
            set { SetPropertyValue(nameof(order_date), ref _order_date, value); }
        }
        // 
        // Notes for cOrder : 
        private cCustomer _sender_id;
        [XafDisplayName("Sender Name"), ToolTip("Sender Name")]
        [ImmediatePostData]
        // [Appearance("cOrdersender_id", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public cCustomer sender_id
        {
            get { return _sender_id; }
            set { SetPropertyValue(nameof(sender_id), ref _sender_id, value);
                if (!IsSaving && !IsLoading && value != null)
                {
                    sender_email = value.customer_email;
                    sender_address = value.customer_address;
                }
            }
        }
        // 
        // Notes for cOrder : 
        // private DateTime _sender_name; 
        //[XafDisplayName("sender name"), ToolTip("sender name")] 
        // [Appearance("cOrdersender_name", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        //public  DateTime sender_name
        //{ 
        //  get { return _sender_name; } 
        //  set { SetPropertyValue(nameof(sender_name), ref _sender_name, value); } 
        //} 
        // 
        // Notes for cOrder : 
        private String _sender_email;
        [XafDisplayName("Sender Email"), ToolTip("Sender Email")]
        [Appearance("cOrdersender_email", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String sender_email
        {
            get { return _sender_email; }
            set { SetPropertyValue(nameof(sender_email), ref _sender_email, value); }
        }
        // 
        // Notes for cOrder : 
        private String _sender_address;
        [XafDisplayName("Sender Address"), ToolTip("Sender Address")]
        [Appearance("cOrdersender_address", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String sender_address
        {
            get { return _sender_address; }
            set { SetPropertyValue(nameof(sender_address), ref _sender_address, value); }
        }
        // 
        // Notes for cOrder : 
        private cCustomer _recipient_id;
        [XafDisplayName("Recipient Name"), ToolTip("Recipient Name")]
        [ImmediatePostData]
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
                    recipient_email = value.customer_email;
                    recipient_address = value.customer_address;
                }
            }
        }
        // 
        // Notes for cOrder : 
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
        // Notes for cOrder : 
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
        // 
        // Notes for cOrder : 
        private String _type_good;
        [XafDisplayName("Types of good"), ToolTip("Types of good")]
        // [Appearance("cOrdertype_good", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String type_good
        {
            get { return _type_good; }
            set { SetPropertyValue(nameof(type_good), ref _type_good, value); }
        }
        // 
        // Notes for cOrder : 
        private double _weight_good;
        [XafDisplayName("Weight of good"), ToolTip("Weight of good")]
        // [Appearance("cOrderweight_good", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public double weight_good
        {
            get { return _weight_good; }
            set { SetPropertyValue(nameof(weight_good), ref _weight_good, value); }
        }
        // 
        // Notes for cOrder : 
        private double _shipping_cost;
        [XafDisplayName("Shipping Cost"), ToolTip("Shipping Cost")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        [ImmediatePostData]
        // [Appearance("cOrdershipping_cost", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public double shipping_cost
        {
            get { return _shipping_cost; }
            set { SetPropertyValue(nameof(shipping_cost), ref _shipping_cost, value); }
        }
        // 
        // Notes for cOrder : 
        private double _tax;
        [XafDisplayName("Tax"), ToolTip("Tax")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        [ImmediatePostData]
        // [Appearance("cOrdertax", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public double tax
        {
            get { return _tax; }
            set { SetPropertyValue(nameof(tax), ref _tax, value); }
        }
        // 
        // Notes for cOrder : 
        private double _gst;
        [XafDisplayName("GST"), ToolTip("Goods and Services Tax")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        [PersistentAlias("weight_good*tax")]
        // [Appearance("cOrdergst", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public  double gst
     { 
       get {
                object tempObject = EvaluateAlias(nameof(gst));
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
                return _gst; } 
       //set { SetPropertyValue(nameof(gst), ref _gst, value); } 
     } 
     // 
     // Notes for cOrder : 
     private double _total; 
     [XafDisplayName("Total"), ToolTip("Total from Shipping Cost, Tax, and GST")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        [PersistentAlias("shipping_cost+tax+gst")]
        [Appearance("cOrdertotal", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public  double total
     { 
       get {
                object tempObject = EvaluateAlias(nameof(total));
                if(tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
                return _total; } 

       set { SetPropertyValue(nameof(total), ref _total, value); } 
     } 
   } 
} 
