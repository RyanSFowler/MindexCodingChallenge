using challenge.Models;
using System;
using System.Collections.Generic;

namespace challenge.Services
{
    public interface ICompensationService
    {
        Compensation GetById(String id);
        Compensation Create(Compensation employee);
    }
}
