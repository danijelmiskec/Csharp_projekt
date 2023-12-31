﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt {
    public partial class StartForm : Form {
        private Admins Admins = new Admins();
        public TempList podaci1 = new TempList();
        Orders orders = new Orders();
        public StartForm() {
            InitializeComponent();
            UsernameLbl.Hide();
            UsernameTb.Hide();
            PasswordLbl.Hide();
            PasswordTb.Hide();
            AddProductBtn.Hide();
            orders.PripremaSe += OsvijeziPripremu;
            orders.PredajeSe += OsvijeziPredaju;
        }

        private void OsvijeziPripremu(object sender) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    LbPriprema.Items.Clear();
                    LbPriprema.Items.AddRange(orders.DohvatiUPripremi().ToArray());
                }));
            }
        }

        private void OsvijeziPredaju(object sender) {
            if (InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    OsvjeziFormu();
                }));
            }
        }
        public void OsvjeziFormu() {
            LbPriprema.Items.Clear();
            LbPriprema.Items.AddRange(orders.DohvatiUPripremi().ToArray());
            LbPredaja.Items.Clear();
            LbPredaja.Items.AddRange(orders.DohvatiZaPredaju().ToArray());
            CurrentOrderLb.Items.Clear();
            if (podaci1.DataList != null) {
                foreach (var t in podaci1.DataList) {
                    CurrentOrderLb.Items.Add(t.ToString());
                }

            };
        }



        private void BurgerPictureBox_Click(object sender, EventArgs e) {
            var burgerForm = new BurgersForm(this);
            burgerForm.Show();
            this.Hide();
        }

        private void PommesPictureBox_Click(object sender, EventArgs e) {
            var pommesForm = new PommesForm(this);
            pommesForm.Show();
            this.Hide();
        }

        private void DessertPictureBox_Click(object sender, EventArgs e) {
            var dessertsForm = new DessertsForm(this);
            dessertsForm.Show();
            this.Hide();
        }

        private void DrinkPictureBox_Click(object sender, EventArgs e) {
            var drinksForm = new DrinksForm(this);
            drinksForm.Show();
            this.Hide();
        }

        private void CoffeePictureBox_Click(object sender, EventArgs e) {
            var caffeForm = new CoffeeForm(this);
            caffeForm.Show();
            this.Hide();
        }

        //Button za prijavu admina
        private void AdminLoginBtn_Click(object sender, EventArgs e) {
            //provjera dal su username i password textBox vidljivi
            if (UsernameTb.Visible) {
                //provjera da li su prazni textBox-ovi za username i password
                if ((UsernameTb.Text == null || UsernameTb.Text == string.Empty) || (PasswordTb.Text == null || PasswordTb.Text == string.Empty)) {
                    MessageBox.Show("Niste unjeli username ili password!!",
                                    "Greska kod prijave",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                } else {
                    //Provjera username i password za svaki admin zapis u tablici
                    if (Admins.GetAdmin(UsernameTb.Text, PasswordTb.Text) != null) {
                        AddProductBtn.Show();
                    } else {
                        MessageBox.Show("Pogrešan username ili password!!",
                                        "Greska kod prijave",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        UsernameTb.Clear();
                        PasswordTb.Clear();
                    }
                }
            } else {
                UsernameLbl.Show();
                UsernameTb.Show();
                PasswordLbl.Show();
                PasswordTb.Show();
            }
        }

        private void AddProductBtn_Click(object sender, EventArgs e) {
            var AddProductForm = new AddProductForm();
            AddProductForm.ShowDialog();
            this.Close();
        }

        private void EndOrderBtn_Click(object sender, EventArgs e) {
            if (podaci1.DataList.Count != 0) {
                orders.NewOrder(podaci1);
                CurrentOrderLb.Items.Clear();
            } else {
                MessageBox.Show("Narudzba je prazna");
            }
        }

        private void CancleOrderBtn_Click(object sender, EventArgs e) {
            MessageBox.Show("Narudzba je ponistena!!");
            podaci1.DataList.Clear();
            CurrentOrderLb.Items.Clear();
        }

    }
}
