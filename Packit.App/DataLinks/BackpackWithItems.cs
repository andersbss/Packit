﻿using Packit.App.Helpers;
using Packit.Model;
using Packit.Model.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataLinks
{
    public class BackpackWithItems : Observable
    {
        private ObservableCollection<Model.Item> items;
        private Backpack backpack;

        public Backpack Backpack
        {
            get => backpack;
            set
            {
                if (backpack == value) return;
                backpack = value;
                OnPropertyChanged(nameof(Backpack));
            }
        }
        public ObservableCollection<Model.Item> Items
        {
            get => items;
            set
            {
                if (items == value) return;
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public BackpackWithItems(Backpack backpack)
        {
            Backpack = backpack;
            Items = new ObservableCollection<Model.Item>();
        }
    }
}
