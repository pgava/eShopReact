﻿using eShopModular.Common.Domain;

namespace eShopModular.Domain.Countries;

public class CountryId : TypedIdValueBase
{
    public CountryId(Guid value)
        : base(value)
    {
    }
}