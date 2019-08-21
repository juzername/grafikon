using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Signals
{
    public partial class GraphicsSignalView : UserControl, IView
    {
        /// <summary>
        /// skálatényező a nagyításhoz
        /// </summary>
        private double scaleFactor = 1;

        /// <summary>
        /// A view sorszáma
        /// </summary>
        private int viewNumber;

        /// <summary>
        /// A view sorszáma
        /// </summary>
        public int ViewNumber
        {
            get { return viewNumber; }
            set { viewNumber = value; }
        }

        /// <summary>
        /// A dokumentum, melynek adatait a nézet megjeleníti.
        /// </summary>
        private SignalDocument document;

        /// <summary>
        /// default konstruktor
        /// </summary>
        public GraphicsSignalView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// konstruktor SignalDocument paraméterrel
        /// </summary>
        /// <param name="document"></param>
        public GraphicsSignalView(SignalDocument document)
        {
            InitializeComponent();
            this.document = document;
        }

         /// <summary>
         /// a dokumentum, amelynek adatait a nézet megjeleníti
         /// </summary>
         /// <returns></returns>
        public Document GetDocument()
        {
            return document;
        }

        /// <summary>
        /// A View interfész Update műveletánek implementációja.
        /// </summary>
        public void Update()
        {
            Invalidate();
        }

        /// <summary>
        /// A UserControl.Paint felüldefiniálása, ebben rajzolunk.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //rajzoláshoz piros, szaggatott toll, nyíl végződéssel
            Pen pen = new Pen(Color.Red, 1);
            pen.DashStyle = DashStyle.Dash;
            pen.EndCap = LineCap.ArrowAnchor;

            //y tengely (0;0) pontból a teljes képernyőhosszon
            e.Graphics.DrawLine(pen, 0, ClientSize.Height, 0, 0);

            //x tengely a képernyő közepére
            e.Graphics.DrawLine(pen, 0, ClientSize.Height / 2, ClientSize.Width, ClientSize.Height / 2);

           
            //az első időbélyeg dátum formátumban
            DateTime timeBefore = document.Signals[0].TimeStamp;
            //az első érték
            double valueBefore = document.Signals[0].Value;
                        
            //a legnagyobb felvett abszolútérték -> a grafikon kirajzolásakor ennek ki kell férnie
            double maxValue = valueBefore;
            foreach(SignalValue sv in document.Signals)
            {
                if (Math.Abs(sv.Value) > maxValue)
                    maxValue = Math.Abs(sv.Value);
            }

            //időbélyegekhez skálatényező -> 1 másodperc  ennyi pixelnek felel meg
            //az ablak szélességének 0.7-szeresét (hogy ne a legszélén legyenek az értékek) osztjuk a legnagyobb és legkisebb időérték különbségével
            double pixelPerSec = Convert.ToDouble(ClientSize.Width*0.7) / ((document.Signals[document.Signals.Count - 1].TimeStamp.Ticks - document.Signals[0].TimeStamp.Ticks) /10000000.0f);

            //értékhez skálatényező -> 1 egységnyi érték ennyi pixelnek felel meg
            //az ablak magasságának 0.7-szeresét (hogy ne a legszélén legyenek az értékek) osztjuk a legnagyobb abszolútérték kétszeresével, hiszen negatív értékeket is felvehet
            double pixelPerValue = Convert.ToDouble(ClientSize.Height*0.7) / (maxValue*2);

                 
            //előző x és y koordináták a vonal kirajzolásához
            int xBefore = 0;
            //az első y koordináta számítása: az x tengely helyéhez képest a skálázott értékkel eltolva
            //a pixelPerVaule-val skálázott értéket a nagyítási skálatényezővel is szorozni kell
            int yBefore = Convert.ToInt32(ClientSize.Height / 2 - (valueBefore * pixelPerValue * scaleFactor));

            //kék ecset
            SolidBrush brush = new SolidBrush(Color.Blue);

            //az első pont kirajzolása az x = 0 helyre
            e.Graphics.FillRectangle(brush, new Rectangle(xBefore, yBefore, 3, 3));

            //az értékekeket ábrázoló pontok kirajzolása
            for (int i = 1; i < document.Signals.Count; i++)
            {
                SignalValue sv = document.Signals[i];

                //az aktuális és első időbélyeg különbsége
                TimeSpan timeSpan = sv.TimeStamp.Subtract(timeBefore);
                //másodpercben
                double timeDifference = timeSpan.Ticks / 10000000.0;

                //az x koordináta az aktuális időbélyeg elsővel való eltolása lesz a skálatényezővel szorozva
                //a scaleFactor nagyítási skálatényezővel szorozni kell az értéket
                int x_koord = Convert.ToInt32(timeDifference * pixelPerSec * scaleFactor);

                //az y koordináta az x tengelyhez képest eltolva lesz a skálázott érték
                //a scaleFactor nagyítási skálatényezővel szorozni kell az értéket
                int y_koord = Convert.ToInt32(ClientSize.Height / 2 - (sv.Value * pixelPerValue * scaleFactor));

                //a pont kirajzolása         
                e.Graphics.FillRectangle(brush, new Rectangle(x_koord, y_koord, 3, 3));

                //a vonal kirajzolása
                e.Graphics.DrawLine(new Pen(Color.Blue, 1), new Point(xBefore, yBefore), new Point(x_koord, y_koord));

                //az előző x és y értékek átállítása a következő ciklusra
                xBefore = x_koord;
                yBefore = y_koord;
           }
        }

        //ha a '+' gombra kattintunk, 1,2-szeresére nagyítjuk
        private void zoomInB_Click(object sender, EventArgs e)
        {
            scaleFactor *= 1.2;
            Invalidate();
        }

        //ha a '-' gombra kattintunk, 1,2-vel osztjuk a skálatényezőt a méret csökkentéséhez
        private void zoomOutB_Click(object sender, EventArgs e)
        {
            scaleFactor /= 1.2;
            Invalidate();
        }
    }
}
