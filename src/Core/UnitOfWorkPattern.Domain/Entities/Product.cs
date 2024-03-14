﻿using UnitOfWorkPattern.Domain.Common;

namespace UnitOfWorkPattern.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
