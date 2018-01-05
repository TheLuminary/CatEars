﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CatEars.Adapter.UI;
using CatEars.Domain.Repositories;
using CatEars.Adapter.Repositories;
using CatEars.Domain;

namespace CatEars
{
    public partial class Form1 : Form
    {
        ICatRepository catRepository;
        IDictionary<int, CatId> catListIndex;

        public Form1()
        {
            catListIndex = new Dictionary<int, CatId>();
            catRepository = new InMemoryCatRepository();
            InitializeComponent();
            LoadCatList();
        }

        private void uxAddCat_Click(object sender, EventArgs e)
        {
            AddCat addCatForm = new AddCat(catRepository);
            addCatForm.TopLevel = false;
            addCatForm.Saved += AddCatForm_Saved;
            Controls.Add(addCatForm);
            addCatForm.Show();
            addCatForm.BringToFront();
        }

        private void AddCatForm_Saved(object sender, EventArgs e)
        {
            LoadCatList();
        }

        private void EditCatForm_Saved(object sender, EventArgs e)
        {
            LoadCatList();
        }

        private void LoadCatList()
        {
            var cats = catRepository.RetrieveAll();
            catListIndex.Clear();
            foreach(var cat in cats)
            {
                catListIndex.Add(uxCatList.Items.Add(cat.Name), cat.CatId);
            }
        }

        private void uxCatList_DoubleClick(object sender, EventArgs e)
        {
            CatId catId = catListIndex[uxCatList.SelectedIndex];
            Cat cat = catRepository.Retrieve(catId);
            EditCat editCatForm = new EditCat(catRepository, cat);
            editCatForm.TopLevel = false;
            editCatForm.Saved += EditCatForm_Saved;
            Controls.Add(editCatForm);
            editCatForm.Show();
            editCatForm.BringToFront();
        }
    }
}
