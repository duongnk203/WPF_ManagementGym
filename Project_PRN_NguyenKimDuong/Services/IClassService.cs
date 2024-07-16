﻿using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IClassService
    {
        List<Class> GetClassesByTrainerId(int trainerId);
        List<Class> GetClasses();

        Class GetClassByClassId(int classId);
    }
}