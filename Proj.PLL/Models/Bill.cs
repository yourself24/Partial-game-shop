using System;
using System.Collections.Generic;

namespace Proj.PLL.Models;

public partial class Bill
{
    public int Id { get; set; }

    public int? Cart { get; set; }

    public int? Shipment { get; set; }

    public int? PaymentMethod { get; set; }

    public decimal? Price { get; set; }

    public virtual Cart? CartNavigation { get; set; }

    public virtual UserPayment? PaymentMethodNavigation { get; set; }

    public virtual Shipping? ShipmentNavigation { get; set; }
}
