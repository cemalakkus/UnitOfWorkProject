﻿namespace UnitOfWorkPattern.Application.Dtos;

public class ProductCreateRequest
{  
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
