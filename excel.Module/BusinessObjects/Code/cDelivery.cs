// Class Name : cDelivery.cs 
// Project Name : EXELS 
// Last Update : 4/19/2022 3:16:15 PM  
 
// Gen Code Version : 1.0.0.0  
 
// Revise ==> Revice date : 4/19/2022 3:16:15 PM 
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
    [DefaultProperty("delivery_number")]
    [NavigationItem("Shipment")]
    // Standard Document
    [System.ComponentModel.DisplayName("Delivery")]
    public class cDelivery : XPObject
    {
        public cDelivery(Session session) : base(session)
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
            delivery_number = Number(sDate);
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
        [Appearance("VisiblecDeliveryOID", Visibility = ViewItemVisibility.Hide)]
        public int Oid
        {
            get { return base.Oid; }
            set { base.Oid = value; }
        }
        // 
        // Notes for cDelivery : 
        private cOrder _order_number;
        [XafDisplayName("Order Number"), ToolTip("Order Number")]
        // [Appearance("cDeliverydelivery_date", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        [ImmediatePostData]
        public cOrder order_number
        {
            get { return _order_number; }
            set
            {
                SetPropertyValue(nameof(order_number), ref _order_number, value);
                if (!IsSaving && !IsLoading && value != null)
                {
                    order_date = value.order_date;
                    sender_id = value.sender_id;
                    sender_address = value.sender_address;
                    recipient_id = value.recipient_id;
                    recipient_address = value.recipient_address;
                    shipping_cost = value.shipping_cost;
                    total = value.total;

                }
            }
        }

        private String _delivery_number;
        [XafDisplayName("Delivery Number"), ToolTip("Delivery Number")]
        [Appearance("cOrderdelivery_number", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public String delivery_number
        {
            get { return _delivery_number; }
            set { SetPropertyValue(nameof(delivery_number), ref _delivery_number, value); }
        }

        public virtual string Number(DateTime sDate)
        {

            string sNumer = "";
            int sRun = 1;
            byte iYear = (byte)((sDate.Year - 2022));
            string sYear = $"DND.{sDate.ToString("yyMM")}";
            XPCollection<cDelivery> xpDM = new XPCollection<cDelivery>(Session);
            string sNumberMax = "";
            try
            {
                sNumberMax = xpDM
               //SelectMany(c => c.).
               .Where(o => o.delivery_number.Substring(0, 8).ToUpper().Trim() == sYear.ToUpper().Trim()
                    && o.delivery_number.Substring(9, 4).All(c => (c >= 48 && c <= 57))
                     )
               .Max(o => o.delivery_number.Trim());
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

        private DateTime _order_date;
        [XafDisplayName("Order Date"), ToolTip("Order Date")]
        [Appearance("cOrderorder_date", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public DateTime order_date
        {
            get { return _order_date; }
            set { SetPropertyValue(nameof(order_date), ref _order_date, value); }
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
                }
            }
        }

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

        private double _shipping_cost;
        [XafDisplayName("Shipping Cost"), ToolTip("Shipping Cost")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        [Appearance("cOrdershipping_cost", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public double shipping_cost
        {
            get { return _shipping_cost; }
            set { SetPropertyValue(nameof(shipping_cost), ref _shipping_cost, value); }
        }

        private double _total;
        [XafDisplayName("Total"), ToolTip("Total from Shipping Cost, Tax, and GST")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        [Appearance("cOrdertotal", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public double total
        {
            get
            {
                return _total;
            }

            set { SetPropertyValue(nameof(total), ref _total, value); }
        }

        private double _total_bill;
        [XafDisplayName("Total Bill"), ToolTip("Total unpaid price")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        [PersistentAlias("total-total_paid")]
        [Appearance("cDeliverytotal_bill", Enabled = false)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public double total_bill
        {
            get
            {
                object tempObject = EvaluateAlias(nameof(total_bill)); ;
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
                return _total_bill;
            }
            set
            {
                SetPropertyValue(nameof(total_bill), ref _total_bill, value);
            }
        }

        // 
        // Notes for cDelivery : 
        private DateTime _delivery_date;
        [XafDisplayName("Delivery Date"), ToolTip("Parcel Delivery Date")]
        // [Appearance("cDeliverydelivery_date", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public DateTime delivery_date
        {
            get { return _delivery_date; }
            set { SetPropertyValue(nameof(delivery_date), ref _delivery_date, value); }
        }
        // 
        // Notes for cDelivery : 
        private cDriver _id_driver;
        [XafDisplayName("Driver's name"), ToolTip("Driver's name")]
        [ImmediatePostData]
        // [Appearance("cDeliverydriver_name", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public cDriver id_driver
        {
            get { return _id_driver; }
            set
            {
                SetPropertyValue(nameof(id_driver), ref _id_driver, value);
                if(!IsLoading && !IsSaving)
                {
                    if(value.truck_number != null)
                    {
                        truck_number = value.truck_number;
                    }
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
        // Notes for cDelivery : 
        private eStatusPym _status_pym;
        [XafDisplayName("Status Payment"), ToolTip("Status Payment")]
        // [Appearance("cDeliverystatus_pym", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        [Size(50)]
        public eStatusPym statuspym
        {
            get { return _status_pym; }
            set { SetPropertyValue(nameof(statuspym), ref _status_pym, value); }
        }
        // 
        // Notes for cDelivery : 
        private double _total_paid;
        [XafDisplayName("Total Paid"), ToolTip("Total price already paid")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        // [Appearance("cDeliverytotal_paid", Enabled = true)]
        // [ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)] 
        // [RuleRequiredField(DefaultContexts.Save)] 
        // [IsSearch(true)]
        public double total_paid
        {
            get { return _total_paid; }
            set { SetPropertyValue(nameof(total_paid), ref _total_paid, value); }
        }
        // 
        // Notes for cDelivery : 
        public enum eStatusPym
        {
            [XafDisplayName("Unpaid")]
            Unpaid = 0,
            [XafDisplayName("Under-payment")]
            UnderPayment = 1,
            [XafDisplayName("Paid")]
            Paid = 2
        }
    }
} 
