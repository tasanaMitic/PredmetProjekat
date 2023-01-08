﻿namespace PredmetProjekat.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandRepository BrandRepository { get; }
        ICategoryRepository CategoryRepository { get; }
    }
}
