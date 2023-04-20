using System.Drawing;
namespace SpaceAdventureBetter
{
    public partial class Form1 : Form
    {
        public TextBox PlanetNum;
        public TextBox CompanyNum;
        public Label PlanetInfo = new Label();
        public Panel CompanyBox = new Panel();
        public Panel PlanetBox = new Panel();
        public Button HelpInfo = new Button();
        public Button AutoCompany = new Button();
        public Button Start = new Button();
        public Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            //Базовая настройка формы
            this.BackgroundImage = null;
            this.BackColor = Color.Black;
            this.Height = 900;
            this.Width = 1500;
            this.MaximumSize = new Size(1500, 900);
            this.MinimumSize = new Size(1500, 900);
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            //Создаем поле для ввода количества планет
            PlanetNum = new TextBox();
            PlanetNum.BorderStyle = BorderStyle.None;
            PlanetNum.Font = new Font(PlanetNum.Font.Name, 15, PlanetNum.Font.Style);
            PlanetNum.Width = 500;
            PlanetNum.Location = new Point((ClientSize.Width / 2) - (PlanetNum.Width / 2), (ClientSize.Height / 2) - PlanetNum.Height);
            PlanetNum.PlaceholderText = "Укажіть кількість планет (3-100)";
            this.Controls.Add(PlanetNum);

            //Создаем поле для ввода количества компаний
            CompanyNum = new TextBox();
            CompanyNum.BorderStyle = BorderStyle.None;
            CompanyNum.Font = new Font(CompanyNum.Font.Name, 15, CompanyNum.Font.Style);
            CompanyNum.Width = 500;
            CompanyNum.Location = new Point((ClientSize.Width / 2) - (CompanyNum.Width / 2), (ClientSize.Height / 2) + PlanetNum.Height);
            CompanyNum.PlaceholderText = "Укажіть кількість компаній (2-100)";
            this.Controls.Add(CompanyNum);

            //Добавляем к событию нажатия кнопки (именно при заполнении поля) новый делегат, в который помещаем метод
            PlanetNum.KeyPress += new KeyPressEventHandler(MethodKey);
            CompanyNum.KeyPress += new KeyPressEventHandler(MethodKey);

            //Настройка поля, которое будет отображать все компании, которые есть в какой-либо планете
            PlanetInfo.Font = new Font(PlanetInfo.Font.Name, 15, PlanetInfo.Font.Style);
            PlanetInfo.BackColor = Color.FromArgb(200, 60, 60, 60);
            PlanetInfo.ForeColor = Color.White;

            //Кнопка для показа основной информации
            HelpInfo.Text = "Основна інформація";
            HelpInfo.BackColor = Color.White;
            HelpInfo.MouseClick += new MouseEventHandler(HelpInfoMethod);
            HelpInfo.FlatAppearance.BorderSize = 0;
            HelpInfo.FlatStyle = FlatStyle.Flat;
            HelpInfo.Font = new Font("sans-serif", 15, HelpInfo.Font.Style);
            HelpInfo.Size = new Size(500, HelpInfo.PreferredSize.Height);
            HelpInfo.Location = new Point(PlanetNum.Location.X, PlanetNum.Location.Y - HelpInfo.Height - 10);
            HelpInfo.BackColor = Color.FromArgb(200, 60, 60, 60);
            HelpInfo.ForeColor = Color.White;
            this.Controls.Add(HelpInfo);

            this.ActiveControl = HelpInfo;
        }


        public void HelpInfoMethod(Object? sendler, MouseEventArgs e)
        {
            MessageBox.Show("Це программа про знаходження найкортшого шляху між планетами, що зєднані за допомогою компаній." +
                "Вам потрібно ввести кількість планет та компаній, а потім призначити кожній планеті ті компанії, що ви забажаєте, " +
                "або ж можете скористатися кнопкою автоматичного заповнення. Для початку програми вам буде необхідно натиснути на кнопу 'Пошук найкоротшого шляху'.");
        }

        public void MethodKey(Object? o, KeyPressEventArgs e)
        {
            //Если нажатая кнопка - Enter, то...
            if (e.KeyChar == (char)Keys.Enter)
            {
                MethodEnter();
            }
        }

        //Метод, который очищает всю форму и заполняет её планетами, если все данные валидны. Он делает все.
        public void MethodEnter()
        {
            try
            {
                //Пытаемся преобразовать строку в число, если выходит то проверяем на валидность
                int PlanetNumber = Convert.ToInt32(PlanetNum.Text);
                int CompanyNumber = Convert.ToInt32(CompanyNum.Text);
                if (3 <= PlanetNumber && PlanetNumber <= 100 && 2 <= CompanyNumber && CompanyNumber <= 100)
                {
                    //Убираем наши поля для ввода количества планет и компаний
                    this.Controls.Remove(PlanetNum);
                    this.Controls.Remove(CompanyNum);
                    this.Controls.Remove(HelpInfo);

                    //Начинаем строить костыльный завод. Хз пока что пусть будет так. Сдесь мы сщитаем размер планет.
                    PlanetBox = new Panel();
                    PlanetBox.Size = new Size(800, 800);
                    PlanetBox.Location = new Point((ClientSize.Width - PlanetBox.Width) / 2, (ClientSize.Height - PlanetBox.Height) / 2);
                    PlanetBox.BackColor = Color.Transparent;
                    this.Controls.Add(PlanetBox);
                    int planetS = PlanetBox.Width * PlanetBox.Height;
                    int sizePlanet = PlanetBox.Width / ((int)Math.Ceiling(PlanetBox.Width / Math.Sqrt(planetS / PlanetNumber)));
                    int maxPlanet = (PlanetBox.Width) / sizePlanet;

                    //Создаем планеты со случайными картинками, и раскладываем их по сетке.
                    int rowsPlanet = 1;
                    int columnsPlanet = 1;
                    for (int i = 0; i < PlanetNumber; i++)
                    {
                        Image img = Properties.Resources._1;
                        switch (rnd.Next(1, 4))
                        {
                            case 1:
                                img = Properties.Resources._1;
                                break;
                            case 2:
                                img = Properties.Resources._2;
                                break;
                            case 3:
                                img = Properties.Resources._3;
                                break;

                        }
                        Planet newPlanet = new Planet(img);
                        newPlanet.picurebox.Size = new Size(sizePlanet, sizePlanet);
                        newPlanet.picurebox.Location = new Point(sizePlanet * (rowsPlanet - 1), sizePlanet * (columnsPlanet - 1));
                        PlanetBox.Controls.Add(newPlanet.picurebox);
                        newPlanet.picurebox.Click += PlanetClick;
                        newPlanet.picurebox.SendToBack();


                        if ((i + 1) % maxPlanet == 0)
                        {
                            columnsPlanet++;
                            rowsPlanet = 0;
                        }
                        rowsPlanet++;
                    }

                    //Первую планету делаем активной, а последнюю конечной
                    Planet.PlanetList[0].isActive = true;
                    Planet.PlanetList[Planet.PlanetList.Count - 1].isEndPlanet = true;

                    //Создаем "подложку" для компаний
                    CompanyBox = new Panel();
                    CompanyBox.BackColor = Color.FromArgb(200, 60, 60, 60);
                    CompanyBox.Size = new Size(320, 800);
                    CompanyBox.Location = new Point(10, (ClientSize.Height - CompanyBox.Height) / 2);
                    this.Controls.Add(CompanyBox);

                    //Создаем заглавный текст для компаний
                    Label companyLabel = new Label();
                    companyLabel.Location = new Point(0, 0);
                    companyLabel.Text = "Компанії";
                    companyLabel.Font = new Font(companyLabel.Font.Name, 25, companyLabel.Font.Style);
                    companyLabel.ForeColor = Color.White;
                    companyLabel.Size = new Size(320, companyLabel.PreferredHeight);
                    companyLabel.TextAlign = ContentAlignment.MiddleCenter;
                    CompanyBox.Controls.Add(companyLabel);

                    //Начинаем создавать компании
                    int maxCompany = 7;
                    int sizeCompany = CompanyBox.Width / maxCompany;
                    int rowsCompany = 1;
                    int columnsCompany = 1;
                    for (int i = 0; i < CompanyNumber; i++)
                    {
                        Company newCompany = new Company();
                        newCompany.picurebox.Size = new Size(sizeCompany, sizeCompany);
                        newCompany.picurebox.Location = new Point(sizeCompany * (rowsCompany - 1), sizeCompany * (columnsCompany - 1) + companyLabel.Height);
                        CompanyBox.Controls.Add(newCompany.picurebox);
                        newCompany.picurebox.BringToFront();
                        newCompany.picurebox.Click += CompanyClick;


                        if ((i + 1)%maxCompany==0)
                        {
                            columnsCompany++;
                            rowsCompany = 0;
                        }
                        rowsCompany++;
                    }

                    //Кнопка, для автоматического заполнения планет компаниями
                    AutoCompany.Text = "Автоматичне заповнення";
                    AutoCompany.BackColor = Color.White;
                    AutoCompany.MouseClick += new MouseEventHandler(AutoCompanyMethod);
                    AutoCompany.FlatAppearance.BorderSize = 0;
                    AutoCompany.FlatStyle = FlatStyle.Flat;
                    AutoCompany.Font = new Font("sans-serif", 15, companyLabel.Font.Style);
                    AutoCompany.Size = new Size(CompanyBox.Width, 45);
                    AutoCompany.Location = new Point(ClientSize.Width - AutoCompany.Width - 10, ClientSize.Height - AutoCompany.Height - CompanyBox.Location.Y);
                    AutoCompany.BackColor = Color.FromArgb(200, 60, 60, 60);
                    AutoCompany.ForeColor = Color.White;
                    this.Controls.Add(AutoCompany);

                    //Кнопка для запуска основной задачи - нахождение кротчайшего пути от активной планеты к последней
                    Start.Text = "Пошук найкоротшого\n шляху";
                    Start.BackColor = Color.White;
                    Start.MouseClick += new MouseEventHandler(StartMethod);
                    Start.FlatAppearance.BorderSize = 0;
                    Start.FlatStyle = FlatStyle.Flat;
                    Start.Font = new Font("sans-serif", 15, companyLabel.Font.Style);
                    Start.Size = new Size(CompanyBox.Width, Start.PreferredSize.Height);
                    Start.Location = new Point(ClientSize.Width - Start.Width - 10, AutoCompany.Location.Y - Start.Height - 5);
                    Start.BackColor = Color.FromArgb(200, 60, 60, 60);
                    Start.ForeColor = Color.White;
                    this.Controls.Add(Start);

                    //Задаем фон
                    this.BackgroundImage = Properties.Resources.back;
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть числа! (1 - 100)");
            }
        }

        //Метод, который обрабатывает нажатие на компанию
        public void CompanyClick(Object? sendler, EventArgs e)
        {
            PictureBox? currentPictureBox = sendler as PictureBox;
            for (int i = 0; i < Company.CompanyList.Count; i++)
            {
                if (Company.CompanyList[i].picurebox == currentPictureBox)
                {
                    if (Company.CompanyList[i].isActive)
                    {
                        CompanyClean();
                        break;
                    }
                    else
                    {
                        CompanyClean();
                        Company.CompanyList[i].isActive = true;
                        Company.CompanyList[i].picurebox.BorderStyle = BorderStyle.Fixed3D;
                        break;
                    }
                }
            }
        }

        //Метод, который очищает все компании, делая их не активными
        public void CompanyClean()
        {
            for (int i = 0; i < Company.CompanyList.Count; i++)
            {
                Company.CompanyList[i].isActive = false;
                Company.CompanyList[i].picurebox.BorderStyle = BorderStyle.None;
            }
        }

        //Метод, который обрабатывает нажатие на планету
        public async void PlanetClick(Object? sendler, EventArgs e)
        {
            PictureBox? currentPictureBox = sendler as PictureBox;
            Planet currentPlanet = new Planet();
            for (int i = 0; i < Planet.PlanetList.Count; i++)
            {
                if (Planet.PlanetList[i].picurebox == currentPictureBox)
                {
                    currentPlanet = Planet.PlanetList[i];
                    break;
                }
            }
            if (currentPictureBox != null)
            {
                for (int i = 0; i < Company.CompanyList.Count; i++)
                {
                    //Елси какая-то компания активна, то при нажатии добавляем эту компанию к планете
                    if (Company.CompanyList[i].isActive)
                    {
                        Color preventColor = Color.White;
                        for (int j = 0; j < currentPlanet.CompanyForPlanet.Count; j++)
                        {
                            //Если планета уже содержит эту компанию, то убираем её из планеты
                            if (currentPlanet.CompanyForPlanet[j] == Company.CompanyList[i])
                            {
                                currentPlanet.CompanyForPlanet.Remove(Company.CompanyList[i]);
                                preventColor = currentPictureBox.BackColor;
                                currentPictureBox.BackColor = Color.FromArgb(200, 209, 77, 99);
                                await Task.Delay(300);
                                currentPictureBox.BackColor = preventColor;
                                return;
                            }
                        }
                        currentPlanet.CompanyForPlanet.Add(Company.CompanyList[i]);
                        preventColor = currentPictureBox.BackColor;
                        currentPictureBox.BackColor = Color.FromArgb(200, 113, 227, 129);
                        await Task.Delay(300);
                        currentPictureBox.BackColor = preventColor;
                        return;
                    }
                }
                //Если нет активной компании, то показываем информацию про планету
                PlanetInfoMethod(currentPictureBox, new List<Planet>());
            }
        }

        //Метод, для отображения всех компаний в планете или кротчайшего пути
        public void PlanetInfoMethod(PictureBox picturebox, List<Planet> wayList)
        {
            string PlanetText = "";
            if (wayList.Count >= 1)
            {
                PlanetText = $"Найкоротший шлях: {wayList.Count - 1}\n";
                for (int i = 0; i < wayList.Count; i++)
                {
                    PlanetText += $"{wayList[i].name}\n";
                    if ((i + 1) != wayList.Count)
                    {
                        PlanetText += "↓\n";
                    }
                }
                using (StreamWriter writer = new StreamWriter(@".\LastWay.txt", false))
                {
                    writer.Write(PlanetText);
                }
            }
            else
            {
                Planet currentPlanet = new Planet();
                for (int i = 0; i < Planet.PlanetList.Count; i++)
                {
                    if (Planet.PlanetList[i].picurebox == picturebox)
                    {
                        currentPlanet = Planet.PlanetList[i];
                        break;
                    }
                }
                PlanetText = "";
                if (currentPlanet.isActive)
                {
                    PlanetText += "Активна\n";
                }
                else if (currentPlanet.isEndPlanet)
                {
                    PlanetText += "Кінцева\n";
                }
                PlanetText += $"{currentPlanet.name} \nКоманії: \n";
                if (currentPlanet.CompanyForPlanet.Count >= 1)
                {
                    for (int i = 0; i < currentPlanet.CompanyForPlanet.Count; i++)
                    {
                        PlanetText += $"№{currentPlanet.CompanyForPlanet[i].name} ";
                        if ((i + 1) % 4 == 0)
                        {
                            PlanetText += "\n";
                        }
                    }
                }
                else
                {
                    PlanetText += "Немає компаній";
                }
            }
            PlanetInfo.Text = PlanetText;
            PlanetInfo.Size = new Size(CompanyBox.Width, PlanetInfo.PreferredHeight);
            PlanetInfo.Location = new Point(CompanyBox.Width + PlanetBox.Width + 30, CompanyBox.Location.Y);
            this.Controls.Add(PlanetInfo);
            PlanetInfo.BringToFront();
        }
        
        //Метод для того чтобы сделать какую-то планету активной
        public void ActivatedPlanet(Planet planet)
        {
            for (int i = 0; i < Planet.PlanetList.Count; i++)
            {
                Planet.PlanetList[i].isActive = false;
            }
            planet.isActive = true;
            planet.isUnChecked = false;
        }

        //Метод для того чтобы сделать какую-то планету конечной точкой
        public void EndedPlanet(Planet planet)
        {
            for (int i = 0; i < Planet.PlanetList.Count; i++)
            {
                Planet.PlanetList[i].isEndPlanet = false;
            }
            planet.isEndPlanet = true;
        }

        //Метод для нахождения активной планеты
        public Planet FindActivePlanet()
        {
            for (int i = 0; i < Planet.PlanetList.Count; i++)
            {
                if (Planet.PlanetList[i].isActive)
                {
                    return Planet.PlanetList[i];
                }
            }
            throw new Exception();
        }

        //Метод для поиска конечной планеты
        public Planet FindEndPlanet()
        {
            for (int i = 0; i < Planet.PlanetList.Count; i++)
            {
                if (Planet.PlanetList[i].isEndPlanet)
                {
                    return Planet.PlanetList[i];
                }
            }
            throw new Exception();
        }

        //Метод который, при нажатии на кнопку автоматически заполняет все планеты компаниями
        public void AutoCompanyMethod(Object? sendler, MouseEventArgs e)
        {
            for (int i = 0; i < Planet.PlanetList.Count; i++)
            {
                Planet.PlanetList[i].RemoveAllCompany();
            }

            //Каждой планете даем по 2 случайных планеты
            for (int i = 0; i < Company.CompanyList.Count; i++)
            {
                while (Company.CompanyList[i].PlanetForCompany.Count < 2)
                {
                    Planet planet = Planet.PlanetList[rnd.Next(0, Planet.PlanetList.Count - 1)];
                    if (Company.CompanyList[i].ExistPlanet(planet) == false)
                    {
                        Company.CompanyList[i].PlanetForCompany.Add(planet);
                    }
                }
            }

            //Стратовую планету соиденяем с несколькими другими
            for (int i = 0; i < 2; i++)
            {
                Planet connectPlanet = Planet.PlanetList[rnd.Next(0, Planet.PlanetList.Count - 1)];
                while (true)
                {
                    Company connectCompany = Company.CompanyList[rnd.Next(0, Company.CompanyList.Count - 1)];
                    if (Planet.PlanetList[0].ExistCompany(connectCompany) == false && connectPlanet.ExistCompany(connectCompany) == false)
                    {
                        Planet.PlanetList[0].AddCompany(connectCompany);
                        connectPlanet.AddCompany(connectCompany);
                        break;
                    }
                }
            }

            //Теперь находим планеты, к которым нет пути и к которым можно добраться
            List<Planet> forNoReason = FindShortWay(FindActivePlanet(), FindEndPlanet());
            List<Planet> goodPlanets = new List<Planet>();
            List<Planet> badPlanets = new List<Planet>();
            for (int i = 0; i < Planet.PlanetList.Count; i++)
            {
                if (Planet.PlanetList[i].way != int.MaxValue)
                {
                    goodPlanets.Add(Planet.PlanetList[i]);
                }
                else
                {
                    badPlanets.Add(Planet.PlanetList[i]);
                }
            }

            //Проходимя по всем "ПЛОХИМ" планетам и подбираем под каждую из них случайную "ХОРОШУЮ" планету, соиденяем их, предворительно очистив все компаниии из плохой планеты
            for (int i = 0; i < badPlanets.Count; i++)
            {
                Planet goodPlanet = goodPlanets[rnd.Next(i, goodPlanets.Count - 1)];
                badPlanets[i].RemoveAllCompany();
                while (true)
                {
                    Company connectCompany = Company.CompanyList[rnd.Next(0, Company.CompanyList.Count)];
                    if (goodPlanet.ExistCompany(connectCompany) == false)
                    {
                        goodPlanet.AddCompany(connectCompany);
                        badPlanets[i].AddCompany(connectCompany);
                        goodPlanets.Add(badPlanets[i]);
                        break;
                    }
                }
            }

            //Конечную планету соиденяем с несколькими другими
            for (int i = 0; i < 2; i++)
            {
                Planet connectPlanet = goodPlanets[rnd.Next(0, goodPlanets.Count - 1)];
                while (true)
                {
                    Company connectCompany = Company.CompanyList[rnd.Next(0, Company.CompanyList.Count - 1)];
                    if (Planet.PlanetList[Planet.PlanetList.Count - 1].ExistCompany(connectCompany) == false && connectPlanet.ExistCompany(connectCompany) == false)
                    {
                        Planet.PlanetList[Planet.PlanetList.Count - 1].AddCompany(connectCompany);
                        connectPlanet.AddCompany(connectCompany);
                        break;
                    }
                }
            }

            ReturnDeffault();

        }

        //Метод, который проверяет, можно ли от одной планеты переместиться на другую
        public bool PlanetCollision(Planet first, Planet second)
        {
            for (int i = 0; i < first.CompanyForPlanet.Count; i++)
            {
                for (int j = 0; j < second.CompanyForPlanet.Count; j++)
                {
                    if (first.CompanyForPlanet[i] == second.CompanyForPlanet[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Метод, который сбрасывает все изменения
        public void ReturnDeffault()
        {
            for (int i = 0; i < Planet.PlanetList.Count; i++)
            {
                Planet.PlanetList[i].isUnChecked = true;
                Planet.PlanetList[i].way = int.MaxValue;
                Planet.PlanetList[i].wayOnPlanet.Clear();
            }
            ActivatedPlanet(Planet.PlanetList[0]);
            EndedPlanet(Planet.PlanetList[Planet.PlanetList.Count - 1]);
        }

        //При нажатии на кнопку пуска...
        public void StartMethod(Object? sendler, MouseEventArgs e)
        {
            List<Planet> way = FindShortWay(FindActivePlanet(), FindEndPlanet());
            ShowWay(way);
        }

        //Метод для основной задачи программы, для поиска кротчайшего пути я использую алгоритм дейкстры
        public List<Planet> FindShortWay(Planet start, Planet end)
        {
            //Сразу проверяем не можно ли переместиться с первой в последнюю планету напрямую
            if (PlanetCollision(start, end))
            {
                return new List<Planet> { start, end };
            }
            start.isUnChecked = false;
            start.way = 0;
            start.wayOnPlanet.Add(start);
            while (true)
            {

                Planet activePlanet = FindActivePlanet();
                //Ищем все планеты на которые можно перемиститься с текущей, и если эта планета не проверена то мы пытаемся проложить ей
                //новый маршрут
                for (int i = 0; i < Planet.PlanetList.Count; i++)
                {
                    if (PlanetCollision(activePlanet, Planet.PlanetList[i]))
                    {
                        if (Planet.PlanetList[i].isUnChecked)
                        {
                            if ((activePlanet.way + 1) < Planet.PlanetList[i].way)
                            {
                                Planet.PlanetList[i].way = activePlanet.way + 1;
                                Planet.PlanetList[i].wayOnPlanet = activePlanet.wayOnPlanet.GetRange(0, activePlanet.wayOnPlanet.Count);
                                Planet.PlanetList[i].wayOnPlanet.Add(Planet.PlanetList[i]);
                            }
                        }
                    }
                }

                //Проверяем остались ли еще непровереные планеты, если нет то заканчиваем программу
                int unCheckedCount = 0;
                for (int i = 0; i < Planet.PlanetList.Count; i++)
                {
                    if (Planet.PlanetList[i].isUnChecked)
                    {
                        unCheckedCount++;
                    }
                }
                if (unCheckedCount == 0)
                {
                    return end.wayOnPlanet;
                }

                //Находим не провереную планету с найменшым маршрутом, и делаем её следующей планетой
                Planet nextPlanet = new Planet();
                for (int i = 0; i < Planet.PlanetList.Count; i++)
                {
                    if (Planet.PlanetList[i].isUnChecked)
                    {
                        if (Planet.PlanetList[i].way <= nextPlanet.way)
                        {
                            nextPlanet = Planet.PlanetList[i];
                        }
                    }
                }
                ActivatedPlanet(nextPlanet);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Метод для визуализации телепортаций
        public async void ShowWay(List<Planet> planetList)
        {
            for (int i = 0; i < planetList.Count; i++)
            {
                Color preventColor = planetList[i].picurebox.BackColor;
                planetList[i].picurebox.BackColor = Color.FromArgb(200, 252, 248, 3);
                await Task.Delay(500);
                planetList[i].picurebox.BackColor = preventColor;
            }
            PlanetInfoMethod(new Planet().picurebox, planetList);
            
            ReturnDeffault();
        }
    }
}