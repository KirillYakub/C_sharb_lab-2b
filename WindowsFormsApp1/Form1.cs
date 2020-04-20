using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    //GUI;
    public partial class Form1 : Form
    {
        public Form1()
        { InitializeComponent(); }

        public void Form1_Load(object sender, EventArgs e)
        { }

        //генерирование координат треугольников;
        public void button1_Click(object sender, EventArgs e)
        {
            int size;
            int count = 0;

            try
            {
                size = Convert.ToInt32(textBox_size.Text);

                Isoc isoc = new Isoc();

                Triangle number = new Triangle();

                Triangle[] arr = new Triangle[size];

                Random random = new Random();

                for (int i = 0; i < arr.Length; i++)
                {
                    do
                    {
                        arr[i] = new Triangle();

                        arr[i].pointXA = random.Next(0, 30);

                        arr[i].pointYA = random.Next(0, 30);

                        //циклы для проверки ввода координат - координаты точек не могут совпадать
                        do
                        {

                            arr[i].pointXB = random.Next(0, 30);

                        } while (arr[i].pointXB == arr[i].pointXA);

                        do
                        {

                            arr[i].pointYB = random.Next(0, 30);

                        } while (arr[i].pointYB == arr[i].pointYA);

                        do
                        {

                            arr[i].pointXC = random.Next(0, 30);

                        } while (arr[i].pointXC == arr[i].pointXB || arr[i].pointXC == arr[i].pointXA);

                        do
                        {

                            arr[i].pointYC = random.Next(0, 30);

                        } while (arr[i].pointYC == arr[i].pointYB || arr[i].pointYC == arr[i].pointYA);

                        //находим длины сторон по углам;
                        arr[i].sideAB = Math.Abs(Math.Sqrt((number.VariableA(arr, i) * number.VariableA(arr, i)) + (number.VariableB(arr, i) * number.VariableB(arr, i))));
                        arr[i].sideBC = Math.Abs(Math.Sqrt((number.VariableB1(arr, i) * number.VariableB1(arr, i)) + (number.VariableC(arr, i) * number.VariableC(arr, i))));
                        arr[i].sideAC = Math.Abs(Math.Sqrt((number.VariableA1(arr, i) * number.VariableA1(arr, i)) + (number.VariableC1(arr, i) * number.VariableC1(arr, i))));

                    } while (!number.isCorrect(arr, i));

                }

                //подсчет и вывод информ. о равнобедренном треугольнике с макс. площадью;
                number.Calculate(size, arr);

                for (int i = 0; i < arr.Length; i++)
                {
                    if (number.beIsosceles(size, arr) >= 0 && number.beIsosceles(size, arr) == i)
                    {
                        richTextBox2.Text += ($"Равнобедренный треугольник под номером {i + 1} имеет найбольшую площадь: {arr[number.beIsosceles(size, arr)].area} см^2" + "\n\n");
                        count++;
                        break;
                    }
                }

                if (count == 0)
                {
                    richTextBox2.Text += ($"Среди данных треугольников нет равнобедренного для подсчета максимальной площади" + "\n\n");
                }

                //вывод информации;
                for (int i = 0; i < arr.Length; i++)
                {
                    richTextBox1.Text += ($"Треугольник под номером {i + 1}" + "\n");
                    richTextBox1.Text += (($"Сторона АB = {Math.Round(arr[i].sideAB, 3)}, BC = {Math.Round(arr[i].sideBC, 3)}, AC = {Math.Round(arr[i].sideAC, 3)}" + "\n"));
                    richTextBox1.Text += (($"Площадь = {Math.Round(arr[i].area, 3)}, Периметр = {Math.Round(arr[i].perimeter, 3)}") + "\n");
                    richTextBox1.Text += (($"Угол А = {Math.Round(arr[i].angleA, 3)}, B = {Math.Round(arr[i].angleB, 3)}, C = {Math.Round(arr[i].angleC, 3)}") + "\n\n");

                    if (isoc.isoc(arr, i))
                    {
                        richTextBox2.Text += ($"Треугольник под номером {i + 1} равнобедренный" + "\n\n");
                    }

                    else
                    {
                        richTextBox2.Text += ($"Треугольник под номером {i + 1} не является равнобедренным" + "\n\n");
                    }
                }

                //поиск подобных треугольников;
                for (int i = 0; i < arr.Length; i++)
                {
                    richTextBox3.Text += ($"Подобные {i + 1}-му: ");

                    count = 0;

                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if ((arr[i].sideAB > arr[j].sideAB) && (arr[i].sideBC > arr[j].sideBC) && (arr[i].sideAC > arr[j].sideAC))
                        {
                            double a = arr[i].sideAB / number.differenceA1(arr, i, j), a1 = arr[j].sideAB / number.differenceA1(arr, i, j), b = arr[i].sideBC / number.differenceA2(arr, i, j);
                            double b1 = arr[j].sideBC / number.differenceA2(arr, i, j), c = arr[i].sideAC / number.differenceA3(arr, i, j), c1 = arr[j].sideAC / number.differenceA3(arr, i, j);
                            if ((a - (int)a < Double.Epsilon) && (a1 - (int)a1 < Double.Epsilon) && (b - (int)b < Double.Epsilon) && (b1 - (int)b1 < Double.Epsilon) && (c - (int)c < Double.Epsilon) && (c1 - (int)c1 < Double.Epsilon))
                            {
                                richTextBox3.Text += ($"{j + 1}; ");
                                count++;
                                continue;
                            }
                        }

                        if ((arr[i].sideAB < arr[j].sideAB) && (arr[i].sideBC < arr[j].sideBC) && (arr[i].sideAC < arr[j].sideAC) && i != j)
                        {
                            double a = arr[i].sideAB / number.differenceB1(arr, i, j), a1 = arr[j].sideAB / number.differenceB1(arr, i, j), b = arr[i].sideBC / number.differenceB2(arr, i, j);
                            double b1 = arr[j].sideBC / number.differenceB3(arr, i, j), c = arr[i].sideAC / number.differenceB3(arr, i, j), c1 = arr[j].sideAC / number.differenceB3(arr, i, j);
                            if ((a - (int)a < Double.Epsilon) && (a1 - (int)a1 < Double.Epsilon) && (b - (int)b < Double.Epsilon) && (b1 - (int)b1 < Double.Epsilon) && (c - (int)c < Double.Epsilon) && (c1 - (int)c1 < Double.Epsilon))
                            {
                                richTextBox3.Text += ($"{j + 1}; ");
                                count++;
                                continue;
                            }
                        }
                    }

                    if (count == 0)
                    {
                        richTextBox3.Text += ("Нет подобных");
                    }

                    richTextBox3.Text += ("\n\n");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            Console.ReadLine();
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        { }

        public void label1_Click(object sender, EventArgs e)
        { }

        public void richTextBox1_TextChanged(object sender, EventArgs e)
        { }

        public void button2_Click(object sender, EventArgs e)
        { this.Close(); }

        public void richTextBox2_TextChanged(object sender, EventArgs e)
        { }

        public void richTextBox3_TextChanged(object sender, EventArgs e)
        { }

        public void label2_Click(object sender, EventArgs e)
        { }

        public void label4_Click(object sender, EventArgs e)
        { }
    }

    class Isoc : Triangle
    {
        //производный класс для проверки равнобедренности треугольника;
        public bool isoc(Triangle[] arr, int i)
        {
            sideAB = arr[i].sideAB;
            sideBC = arr[i].sideBC;
            sideAC = arr[i].sideAC;

            if (sideAB == sideBC || sideBC == sideAC || sideAB == sideAC)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Triangle
    {
        //необходимые данные;
        public double pointXA, pointYA, pointXB, pointYB, pointXC, pointYC, sideAB, sideBC, sideAC, diffA1, diffA2, diffA3;
        public double area, perimeter, half_perimeter, angleA, angleB, angleC, A, B, B1, C, A1, C1, diffB1, diffB2, diffB3;
       
        public double differenceA1(Triangle[] arr, int i, int j)
        {
            return diffA1 = arr[i].sideAB - arr[j].sideAB;
        }

        public double differenceA2(Triangle[] arr, int i, int j)
        {
            return diffA2 = arr[i].sideBC - arr[j].sideBC;
        }

        public double differenceA3(Triangle[] arr, int i, int j)
        {
            return diffA3 = arr[i].sideAC - arr[j].sideAC;
        }

        public double differenceB1(Triangle[] arr, int i, int j)
        {
            return diffB1 = arr[j].sideAB - arr[i].sideAB;
        }

        public double differenceB2(Triangle[] arr, int i, int j)
        {
            return diffB2 = arr[j].sideBC - arr[i].sideBC;
        }

        public double differenceB3(Triangle[] arr, int i, int j)
        {
            return diffB3 = arr[j].sideAC - arr[i].sideAC;
        }

        public double VariableA(Triangle[] arr, int i)
        {
            return A = arr[i].pointXB - arr[i].pointXA;
        }

        public double VariableB(Triangle[] arr, int i)
        {
            return B = arr[i].pointYB - arr[i].pointYA;
        }

        public double VariableB1(Triangle[] arr, int i)
        {
            return B1 = arr[i].pointXC - arr[i].pointXB;
        }

        public double VariableC(Triangle[] arr, int i)
        {
            return C = arr[i].pointYC - arr[i].pointYB;
        }

        public double VariableA1(Triangle[] arr, int i)
        {
            return A1 = arr[i].pointXC - arr[i].pointXA;
        }

        public double VariableC1(Triangle[] arr, int i)
        {
            return C1 = arr[i].pointYC - arr[i].pointYA;
        }

        //функция проверки условий существования треугольника;
        public bool isCorrect(Triangle[] arr, int i)
        {
            //сумма двух меньших сторон должна быть больше чем большая сторона;
            if ((arr[i].sideAB > arr[i].sideBC) && (arr[i].sideAB > arr[i].sideAC) && (arr[i].sideBC + arr[i].sideAC >= arr[i].sideAB))
            {
                return true;
            }

            if ((arr[i].sideBC > arr[i].sideAB) && (arr[i].sideBC > arr[i].sideAC) && (arr[i].sideAB + arr[i].sideAC >= arr[i].sideBC))
            {
                return true;
            }

            if ((arr[i].sideAC > arr[i].sideAB) && (arr[i].sideAC > arr[i].sideBC) && (arr[i].sideAB + arr[i].sideBC >= arr[i].sideAC))
            {
                return true;
            }

            //если условие выполняется - треугольник либо равнобедренный, либо равносторонний;
            //для этого требуется равенство двух сторон либо всех трех;
            if ((arr[i].sideAB == arr[i].sideBC) || (arr[i].sideBC == arr[i].sideAC) || (arr[i].sideAB == arr[i].sideAC) || ((arr[i].sideAB == arr[i].sideBC) && (arr[i].sideAB == arr[i].sideAC)))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        //функция подсчета площади, периметра и углов;
        public void Calculate(int size, Triangle[] arr)
        {
           
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].perimeter = arr[i].sideAB + arr[i].sideBC + arr[i].sideAC;

                //находим полупериметр для площади;
                arr[i].half_perimeter = (arr[i].sideAB + arr[i].sideBC + arr[i].sideAC) / 2;

                //формула Герона для нахождения площади;
                arr[i].area = Math.Sqrt(arr[i].half_perimeter * (arr[i].half_perimeter - arr[i].sideAB) * (arr[i].half_perimeter - arr[i].sideBC) * (arr[i].half_perimeter - arr[i].sideAC));
                arr[i].area = Math.Abs(arr[i].area);

                //находим углы;
                arr[i].angleA = (arr[i].sideAB * arr[i].sideAB + arr[i].sideAC * arr[i].sideAC - arr[i].sideBC * arr[i].sideBC) / (2 * arr[i].sideAB * arr[i].sideAC) * 180 / Math.PI;
                arr[i].angleA = Math.Abs(arr[i].angleA + 1);

                arr[i].angleB = (arr[i].sideAB * arr[i].sideAB + arr[i].sideBC * arr[i].sideBC - arr[i].sideAC * arr[i].sideAC) / (2 * arr[i].sideAB * arr[i].sideBC) * 180 / Math.PI;
                arr[i].angleB = Math.Abs(arr[i].angleB + 1);

                arr[i].angleC = 180 - (arr[i].angleA + arr[i].angleB);
            }
        }

        //функция поиска равнобедренного треугольника с максимальной площадью;
        public int beIsosceles(int size, Triangle[] arr)
        {
            int count = 0;
            int max_area = -1;

            for (int i = 0; i < arr.Length; i++)
            {
                if ((arr[i].sideAB == arr[i].sideBC) || (arr[i].sideAB == arr[i].sideAC) || (arr[i].sideBC == arr[i].sideAC))
                {
                    if (count == 0)
                    {
                        max_area = i;
                        count++;
                    }

                    if (arr[i].area > arr[max_area].area)
                    {
                        max_area = i;
                    }
                }
            }

            return max_area;
        }
    }
}

