using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
namespace ZeroKreiz
{
   static class AutoGamer
    {
      
        //0-пустая клетка,1-0,2-Х
        //стратегия играем нулями, ставим рейтинг у каждой клетки
        //если в линии два нуля рейтинг максим. 2+2
        //два креста 2+1
        //один ноль +1
        //один крест +1
        //один крест один ноль рейтинг 0

        public static PlacePoint FindPoint(double[,] PlPoint)
        {
            double maxrate = 0;
            int a = 0;
            int b = 0;
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if(PlPoint[i, j]>0&&i==1&&i==j)
                    PlPoint[i, j]=PlPoint[i, j]+0.5;
                    if ( PlPoint[i, j]> maxrate)
                    {
                        
                        maxrate = PlPoint[i, j];
                        a = i;
                        b = j;
                    }
                }
            }
            return new PlacePoint(a,b,maxrate);

        }
        public static Gamefield NextStep(Gamefield fld)
        {
            bool Winplayer=false;
            bool WinAuto = false;
           // PlacePoint[] PlPoint = new PlacePoint[9];
            double[,] PlPoint= new double[3,3];
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    PlPoint[i, j] = 0;
                }
            }

            //столбцы

            int CX = 0;
            for (int i=0;i<=2;i++)
            {
                    int cnt0 = 0;
                    int cntx = 0;
                    double rat = 0;
                    int X = 0;
                for (int j = 0; j <= 2; j++)
                {

                    if (fld.field[i, j] == 1)
                    {
                        cnt0 = cnt0 + 1;
                    }

                    if (fld.field[i, j] == 2)
                    {
                        cntx = cntx + 1;
                        X = j;
                        CX = CX + 1;
                    }

                }

                rat = cntx + cnt0;
                if (cntx == 2)
                {
                    rat = 2 + 3;
                    
                }
                if (cnt0 == 2)
                {
                    rat = 2 + 6;
                    if(cntx==0)
                    WinAuto = true;
                   
                }
                if (cnt0 == 1 && cntx == 1)
                {
                    rat = 0;
                    
                }
                if (cnt0 == 1 && cntx == 1)
                {
                    rat = 0;
                    
                }

                if (cntx == 3)
                    Winplayer = true;

                for (int j = 0; j <= 2; j++)
                {

                    if (fld.field[i, j] == 0)
                    {
                        PlPoint[i, j] = PlPoint[i, j] + rat;
                        if(Math.Abs(X-j)==1&&CX>1 &&fld.diffic==2)
                        PlPoint[i, j] = PlPoint[i, j] + 1.5;

                    }


                }


            }


            //строки

            for (int j = 0; j <= 2; j++)
            {
                int cnt0 = 0;
                int cntx = 0;
                double rat = 0;
                int Y = 0;
                for (int i = 0; i<= 2; i++)
                {

                    if (fld.field[i, j] == 1)
                    {
                        cnt0 = cnt0 + 1;
                    }

                    if (fld.field[i, j] == 2)
                    {
                        cntx = cntx + 1;
                        Y = i;
                    }

                }

                rat = cntx + cnt0;
                if (cntx == 2)
                {
                    rat = 2 + 3;
                    
                }
                if (cnt0 == 2)
                {
                    rat = 2 + 6;
                    if (cntx == 0)
                    WinAuto = true;
                    
                }
                if (cnt0 == 1 && cntx == 1)
                {
                    rat = 0;
                  
                }
                if (cntx == 3)
                    Winplayer = true;

                for (int i = 0; i <= 2; i++)
                {

                    if (fld.field[i, j] == 0)
                    {
                        PlPoint[i, j] = PlPoint[i, j] + rat;
                        if (Math.Abs(Y - i) == 1 && CX > 1&&fld.diffic==2)
                            PlPoint[i, j] = PlPoint[i, j] + 1.5;
                        

                    }


                }


            }


            //диагон
                int cnt0d = 0;
                int cntxd = 0;
                double ratd = 0;

                for (int j = 0; j <= 2; j++)
                {

                    int i = j;

                    if (fld.field[i, j] == 1)
                    {
                        cnt0d = cnt0d + 1;
                    }

                    if (fld.field[i, j] == 2)
                    {
                        cntxd = cntxd + 1;
                    }


                }
                ratd = cntxd + cnt0d;
                if (cntxd == 2)
                {
                    ratd = 2 + 6;
                   
                }
                if (cnt0d == 2)
                {
                    ratd = 2 + 1;
                    if(cntxd == 0)
                    WinAuto = true;
                   
                }
                

               if (cntxd == 1 )
                {
                    ratd = 2;
                    
                }
               if (cntxd == 3)
                   Winplayer = true;
                    


           // }
            for (int j = 0; j <= 2; j++)
            {

                int i = j;
                if (fld.field[i, j] == 0)
                    {
                        PlPoint[i, j] = PlPoint[i, j] + ratd;

                    }

            }




            //diag2
                cnt0d = 0;
                cntxd = 0;
                ratd = 0;
                for (int j = 2; j >= 0; j--)
                {

                    int i = 2 - j;

                    if (fld.field[i, j] == 1)
                    {
                        cnt0d = cnt0d + 1;
                    }

                    if (fld.field[i, j] == 2)
                    {
                        cntxd = cntxd + 1;
                    }


                }
                ratd = cntxd + cnt0d;
                if (cntxd == 2)
                {
                    ratd = 2 + 6;
                    
                }
                if (cnt0d == 2)
                {
                    ratd = 2 + 1;
                    if(cntxd==0)
                    WinAuto = true;
                   
                }
                if (cnt0d == 1 && cntxd == 1)
                {
                    ratd = 0;
                    
                }

                if (cntxd == 1)
                {
                    ratd = 2;

                }

                if (cntxd == 3)
                    Winplayer = true;


          //  }

            if (WinAuto)
                fld.Winner = 1;// "Вы проиграли! Деньги на бочку!";
            if (Winplayer)
                fld.Winner = 2;// "Вы выиграли, поздравляю!";

            if (Winplayer)
                return fld;

            for (int j = 2; j >= 0; j--)
            {

                int i = 2 - j;
                 if (fld.field[i, j] == 0)
                    {
                        PlPoint[i, j] = PlPoint[i, j] + ratd;

                    }
            }



            PlacePoint Point = AutoGamer.FindPoint(PlPoint);
            if(Point.Rate>0)
            fld.field[Point.Numstr, Point.Numcol] = 1;
            return fld;
        }

        public static Winline Drawwinline(Gamefield fld)
        {

            int wa = 0;
            int wp = 0;
            int x1 = 0;
            int x2 = 0;
            int y1 = 0;
            int y2 = 0;


            x1 = 0;
            for (int i = 0; i <= 2; i++)
            {
               wa = 0;
               wp = 0;

               y1 = i;
                for (int j = 0; j <= 2; j++)
                {
                    
                    if (fld.field[i, j] == 1)
                
                       wa++;
                

                    if (fld.field[i, j] == 2)
                   
                       wp++;
                   

                }
                x2 = 2;
                y2 = i;

                if (wa == 3)
                    return new Winline(x1,y1,x2,y2);
                if (wp == 3)
                    return new Winline(x1, y1, x2, y2);


            }


            //столб
            y1 = 0;
            for (int j = 0; j <= 2; j++)
            {
                wa = 0;
                wp = 0;

                for (int i = 0; i <= 2; i++)
                {
                    x1 = j;
                    if (fld.field[i, j] == 1)
                        wa++;

                    if (fld.field[i, j] == 2)
                        wp++;

                }

                x2 = j;
                y2 = 2;

                if (wa == 3)
                    return new Winline(x1, y1, x2, y2);
                if (wp == 3)
                    return new Winline(x1, y1, x2, y2);



                

            }


            //диагон

            wa = 0;
            wp = 0;
            x1 = 0;
            y1 = 0;
            for (int j = 0; j <= 2; j++)
            {

                int i = j;

                if (fld.field[i, j] == 1)
                    wa++;

                if (fld.field[i, j] == 2)
                    wp++;

                x2 = 2;
                y2 = 2;

            }
            if (wa == 3)
                return new Winline(x1, y1, x2, y2);
            if (wp == 3)
                return new Winline(x1, y1, x2, y2); ;



            //diag2
            x1 = 2;
            y1 = 0;
            x2 = 0;
            y2 = 2;
            wa = 0;
            wp = 0;
            for (int j = 2; j >= 0; j--)
            {

                int i = 2 - j;

                if (fld.field[i, j] == 1)
                    wa++;

                if (fld.field[i, j] == 2)
                    wp++;


            }
            if (wa == 3)
                return new Winline(x1, y1, x2, y2);
            if (wp == 3)
                return new Winline(x1, y1, x2, y2); ;

            return new Winline();


        }


    }

    


   class Winline
   {
       public Point Winp1;
       public Point Winp2;
       public Winline()
       { }
       public Winline(int x1,int y1,int x2,int y2)
       {
           Winp1 = new Point(38+x1*77, 38+77*y1);
           Winp2=new Point(38+77*x2,38+77*y2);
       }
   }


    class PlacePoint : IComparable
    {
        private int _numstr;

        public int Numstr
        {
            get { return _numstr; }
            set { _numstr = value; }
        }
        private int _numcol;

        public int Numcol
        {
            get { return _numcol; }
            set { _numcol = value; }
        }
        private double _rate;

        public double Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }

       public PlacePoint(int i, int j , double r)
        {
            Numcol = j;
            Numstr = i;
            Rate = r;

        }



        #region Члены IComparable

        int IComparable.CompareTo(object obj)
        {
            PlacePoint temp =(PlacePoint) obj;
            if (this.Rate > temp.Rate)
                return 1;
            if (this.Rate < temp.Rate)
                return -1;
            else
                return 0;

        }

        #endregion
    }

    [Serializable]
    class Gamefield
    {
       
        public int[,] field = new int[3, 3];
        public int Winner;
        public int diffic;
        public Gamefield(int diff)
        {
            for (int i = 0; i<= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    field[i, j] = 0;
                }
            }
            diffic = diff;

           
        }
    }
}
