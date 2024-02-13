﻿using App.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Alquileres.Events
{
    public sealed record NuevoAlquilerGeneradoDomainEvent(Guid id) : IDomainEvent;
}
