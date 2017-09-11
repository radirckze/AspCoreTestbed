﻿using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IUnitOfWork
    {

        IMovieRepository MovieRepository { get; }

    }
}