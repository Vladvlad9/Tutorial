using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avtosalon {
    class Funkcii {

        public static void ButtonsOFf(bool on, PictureBox[] pb) {
            if (on)
                for (int i = 1; i < pb.Length; i++)
                    pb[i].Enabled = true;
            if (!on)
                for (int i = 1; i < pb.Length; i++)
                    pb[i].Enabled = false;
        }


        public static void Otobrazit(bool skrit, PictureBox[] pb, int nomer) {
            if (skrit) {
                pb[0].Visible = true;
                for (int i = 1; i < pb.Length; i++)
                    if (i != nomer)
                        pb[i].Visible = false;
            }

            if (!skrit) {
                pb[0].Visible = false;
                for (int i = 1; i < pb.Length; i++)
                    pb[i].Visible = true;
            }

        }
    }
}
