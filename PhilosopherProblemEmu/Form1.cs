using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhilosopherProblemEmu
{
    public partial class Form1 : Form
    {
        private int originalPeopleCount = 10;

        int[] outerUIPositionX;
        int[] outerUIPositionY;
        int[] innerUIPositionX;
        int[] innerUIPositionY;

        private static int centerCircleX = 150;
        private static int centerCircleY = 150;
        private int centerCircleXFix = centerCircleX + 150;
        private int centerCircleYFix = centerCircleY + 150;
        private int circleWidth = 300;
        private int circleHeight = 300;
        private int peopleCount;
        private int perDegree;

        //private Image m_imgImage = null;
        private Image chopstickImage = null;
        private Image nothingImage = null;
        private Image thinkingImage = null;
        private Image eatingImage = null;

        private Image nothingStaticImage = null;
        private Image thinkingStaticImage = null;
        private Image hungryingStaticImage = null;
        private Image eatingStaticImage = null;
        private EventHandler m_evthdlAnimator = null;

        DinnerTable dinnerTable = new DinnerTable();

        public static Form1 form1;
        List<CheckBox> leftHandPreferenceCheckBoxes = new List<CheckBox>();
        List<CheckBox> rightHandPreferenceCheckBoxes = new List<CheckBox>();

        public Form1()
        {
            InitializeComponent();
            form1 = this;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            m_evthdlAnimator = new EventHandler(OnImageAnimate);

            InitializeForm(originalPeopleCount);

            SetDinnerSession();

            OutputLogToConsole("程序启动!");
        }

        public void SetDinnerSession()
        {
            dinnerTable.InitDinnerTable(originalPeopleCount);
        }

        private bool isStartButtonSelected = false;

        public void SetDinnerSession(int peopleCount)
        {
            dinnerTable.CloseDinnerTable();

            DisposeCheckBoxes();

            InitializeForm(peopleCount);

            dinnerTable.InitDinnerTable(peopleCount);

            if (isStartButtonSelected)
            {
                dinnerTable.OpenDinnerTable();
            }

        }

        public void InitializeForm(int peopleCount)
        {
            leftHandPreferenceCheckBoxes.Clear();
            rightHandPreferenceCheckBoxes.Clear();

            this.peopleCount = peopleCount;
            this.perDegree = 360 / peopleCount;
            CalculateUIPosition();
            AddCheckBox();

        }

        private void CalculateUIPosition()
        {
            outerUIPositionX = new int[peopleCount];
            outerUIPositionY = new int[peopleCount];
            innerUIPositionX = new int[peopleCount];
            innerUIPositionY = new int[peopleCount];
            for (int k = 0; k < peopleCount; ++k)
            {
                outerUIPositionX[k] = (int)((centerCircleX + circleWidth / 2) + (230 * Math.Cos(perDegree * k * Math.PI / 180)));
                outerUIPositionY[k] = (int)((centerCircleY + circleHeight / 2) + (230 * Math.Sin(perDegree * k * Math.PI / 180)));

                //double rotateDegree = Math.toRadians(perDegree / 2);
                double rotateDegree = (perDegree / 2) * 0.017453292519943295;
                double xsP = (centerCircleX + circleWidth / 2) + 120 * Math.Cos(perDegree * k * Math.PI / 180);
                double ysP = (centerCircleY + circleHeight / 2) + 120 * Math.Sin(perDegree * k * Math.PI / 180);

                innerUIPositionX[k] = (int)(centerCircleXFix + (xsP - centerCircleXFix) * Math.Cos(rotateDegree) - (ysP - centerCircleYFix) * Math.Sin(rotateDegree));
                innerUIPositionY[k] = (int)(centerCircleYFix + (xsP - centerCircleXFix) * Math.Sin(rotateDegree) + (ysP - centerCircleYFix) * Math.Cos(rotateDegree));
            }
        }

        private delegate void OutputLogToConsoleDelegate(string s);
        public void OutputLogToConsole(string s)
        {
            if (textBoxLogArea.InvokeRequired)
            {
                textBoxLogArea.Invoke(new OutputLogToConsoleDelegate(OutputLogToConsole), s);
            }
            else
            {
                textBoxLogArea.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff - ") + s + "\r\n");
            }
            
        }

        public void SettingsConfirm(int minRandomThinkingTime, int maxRandomThinkingTime, int minRandomEatingTime, int maxRandomEatingTime)
        {
            dinnerTable.SetPhilosophersSleepTime(minRandomThinkingTime, maxRandomThinkingTime - minRandomThinkingTime, minRandomEatingTime, maxRandomEatingTime - minRandomEatingTime);
        }

        public void SettingsConfirm(int peopleCount, int minRandomThinkingTime, int maxRandomThinkingTime, int minRandomEatingTime, int maxRandomEatingTime)
        {
            dinnerTable.SetPhilosophersSleepTime(minRandomThinkingTime, maxRandomThinkingTime, minRandomEatingTime, maxRandomEatingTime);
            SetDinnerSession(peopleCount);
            OutputLogToConsole("将哲学家人数设置为了" + peopleCount + "人!");
        }

        private void SaveLogToTxt()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文档(*.txt)|*.txt|所有文件 (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, textBoxLogArea.Text);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Font font = new Font("宋体", 9);
            Brush brush = new SolidBrush(Color.White);
            Rectangle rectangle = new Rectangle(centerCircleX - 10, centerCircleY + 13, circleWidth, circleHeight); //offset in c# x-10, y+13


            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillEllipse(brush, rectangle);

            brush = new SolidBrush(Color.Black);

            for (int k=0;k<peopleCount;++k)
            {
                g.DrawString("哲学家"+(k+1)+"号", font, brush, new Point(outerUIPositionX[k] - 42, outerUIPositionY[k] - 27));
                g.DrawString((dinnerTable.philosophers[k].GetChopstickNumberID()).ToString(), font, brush, new Point(innerUIPositionX[k] - 12, innerUIPositionY[k] - 12));
                g.DrawString((dinnerTable.philosophers[k].GetChopstickPermitsAmount()).ToString(), font, brush, new Point(innerUIPositionX[k] - 12, innerUIPositionY[k] - 22));

                if (dinnerTable.philosophers[k].IsChopstickAvailable())
                {
                    //chopstickImage.paintIcon(this, g, innerUIPositionX[k] - 20, innerUIPositionY[k] - 30);
                    if (chopstickImage != null)
                    {
                        //UpdateImage();
                        e.Graphics.DrawImage(chopstickImage, new Rectangle(innerUIPositionX[k] - 30, innerUIPositionY[k] - 17, chopstickImage.Width, chopstickImage.Height));
                    }
                }

                if (dinnerTable.GetDinnerStatusID() == 1)
                {
                    switch (dinnerTable.philosophers[k].GetStatusID())
                    {
                        case 0:
                            {
                                g.DrawString("空闲", font, brush, new Point(outerUIPositionX[k] + 23, outerUIPositionY[k] - 27));
                                e.Graphics.DrawImage(nothingStaticImage, new Rectangle(outerUIPositionX[k] - 55, outerUIPositionY[k] - 7, nothingStaticImage.Width, nothingStaticImage.Height));
                                break;
                            }
                        case 1:
                            {
                                g.DrawString("思考中...", font, brush, new Point(outerUIPositionX[k] + 23, outerUIPositionY[k] - 27));
                                e.Graphics.DrawImage(thinkingStaticImage, new Rectangle(outerUIPositionX[k] - 55, outerUIPositionY[k] - 7, thinkingStaticImage.Width, thinkingStaticImage.Height));
                                break;
                            }
                        case 2:
                            {
                                g.DrawString("饥饿", font, brush, new Point(outerUIPositionX[k] + 23, outerUIPositionY[k] - 27));
                                e.Graphics.DrawImage(hungryingStaticImage, new Rectangle(outerUIPositionX[k] - 55, outerUIPositionY[k] - 7, hungryingStaticImage.Width, hungryingStaticImage.Height));
                                break;
                            }
                        case 3:
                            {
                                g.DrawString("进食中...", font, brush, new Point(outerUIPositionX[k] + 23, outerUIPositionY[k] - 27));
                                e.Graphics.DrawImage(eatingStaticImage, new Rectangle(outerUIPositionX[k] - 55, outerUIPositionY[k] - 7, eatingStaticImage.Width, eatingStaticImage.Height));
                                break;
                            }
                    }
                }
                else
                {
                    switch (dinnerTable.philosophers[k].GetStatusID())
                    {
                        case 0:
                            {
                                g.DrawString("空闲", font, brush, new Point(outerUIPositionX[k] + 23, outerUIPositionY[k] - 27));
                                UpdateImage();
                                e.Graphics.DrawImage(nothingImage, new Rectangle(outerUIPositionX[k] - 55, outerUIPositionY[k] - 7, nothingImage.Width, nothingImage.Height));
                                break;
                            }
                        case 1:
                            {
                                g.DrawString("思考中...", font, brush, new Point(outerUIPositionX[k] + 23, outerUIPositionY[k] - 27));
                                UpdateImage();
                                e.Graphics.DrawImage(thinkingImage, new Rectangle(outerUIPositionX[k] - 55, outerUIPositionY[k] - 7, thinkingImage.Width, thinkingImage.Height));
                                break;
                            }
                        case 2:
                            {
                                g.DrawString("饥饿", font, brush, new Point(outerUIPositionX[k] + 23, outerUIPositionY[k] - 27));
                                e.Graphics.DrawImage(hungryingStaticImage, new Rectangle(outerUIPositionX[k] - 55, outerUIPositionY[k] - 7, hungryingStaticImage.Width, hungryingStaticImage.Height));
                                break;
                            }
                        case 3:
                            {
                                g.DrawString("进食中...", font, brush, new Point(outerUIPositionX[k] + 23, outerUIPositionY[k] - 27));
                                UpdateImage();
                                e.Graphics.DrawImage(eatingImage, new Rectangle(outerUIPositionX[k] - 55, outerUIPositionY[k] - 7, eatingImage.Width, eatingImage.Height));
                                break;
                            }
                    }
                }
            }


            /*
            if (m_imgImage != null)
            {
                UpdateImage();
                e.Graphics.DrawImage(m_imgImage, new Rectangle(100, 100, m_imgImage.Width, m_imgImage.Height));
            }
            */



        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //m_imgImage = Image.FromFile("resources/img/eating.gif"); // 加载测试用的GIF图片

            //chopstickImage = Image.FromFile("resources/img/chopstick.png");
            //nothingImage = Image.FromFile("resources/img/nothing.gif");
            //thinkingImage = Image.FromFile("resources/img/thinking.gif");
            //eatingImage = Image.FromFile("resources/img/eating.gif");

            //nothingStaticImage = Image.FromFile("resources/img/nothing1.gif");
            //thinkingStaticImage = Image.FromFile("resources/img/thinking1.gif");
            //hungryingStaticImage = Image.FromFile("resources/img/hungrying.gif");
            //eatingStaticImage = Image.FromFile("resources/img/eating1.png");

            chopstickImage = Image.FromStream(ExtractResToStream("resources.img.chopstick.png"));
            nothingImage = Image.FromStream(ExtractResToStream("resources.img.nothing.gif"));
            thinkingImage = Image.FromStream(ExtractResToStream("resources.img.thinking.gif"));
            eatingImage = Image.FromStream(ExtractResToStream("resources.img.eating.gif"));

            nothingStaticImage = Image.FromStream(ExtractResToStream("resources.img.nothing1.gif"));
            thinkingStaticImage = Image.FromStream(ExtractResToStream("resources.img.thinking1.gif"));
            hungryingStaticImage = Image.FromStream(ExtractResToStream("resources.img.hungrying.gif"));
            eatingStaticImage = Image.FromStream(ExtractResToStream("resources.img.eating1.png"));
            BeginAnimate();
        }

        System.IO.Stream ExtractResToStream(string filePath)
        {
            string assembleName = this.GetType().Assembly.GetName().Name;
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("{0}.{1}", assembleName, filePath));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (m_imgImage != null) { }
            StopAnimate();
            //chopstickImage = null;
            //nothingImage = null;
            //thinkingImage = null;
            //eatingImage = null;

            //nothingStaticImage = null;
            //thinkingStaticImage = null;
            //hungryingStaticImage = null;
            //eatingStaticImage = null;
        }

        private void BeginAnimate()
        {
            //if (m_imgImage == null)
            //    return;

            //if (ImageAnimator.CanAnimate(m_imgImage)) { }
            ImageAnimator.Animate(nothingImage, m_evthdlAnimator);
            ImageAnimator.Animate(thinkingImage, m_evthdlAnimator);
            ImageAnimator.Animate(eatingImage, m_evthdlAnimator);

        }

        private void StopAnimate()
        {
            //if (m_imgImage == null)
            //    return;

            //if (ImageAnimator.CanAnimate(m_imgImage)) { }
            ImageAnimator.StopAnimate(nothingImage, m_evthdlAnimator);
            ImageAnimator.StopAnimate(thinkingImage, m_evthdlAnimator);
            ImageAnimator.StopAnimate(eatingImage, m_evthdlAnimator);
        }

        private void UpdateImage()
        {
            //if (m_imgImage == null)
            //    return;

            //if (ImageAnimator.CanAnimate(m_imgImage)) { }
            ImageAnimator.UpdateFrames(nothingImage);
            ImageAnimator.UpdateFrames(thinkingImage);
            ImageAnimator.UpdateFrames(eatingImage);
        }

        private void OnImageAnimate(Object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void AddCheckBox()
        {
            for (int k = 0; k < peopleCount; ++k)
            {
                CheckBox leftHandPreferenceCheckBox = new CheckBox();
                CheckBox rightHandPreferenceCheckBox = new CheckBox();
                leftHandPreferenceCheckBox.Location = new Point(outerUIPositionX[k] - 47, outerUIPositionY[k] + 71);
                rightHandPreferenceCheckBox.Location = new Point(outerUIPositionX[k] + 13, outerUIPositionY[k] + 71);
                leftHandPreferenceCheckBox.Size = new Size(60, 20);
                rightHandPreferenceCheckBox.Size = new Size(60, 20);
                leftHandPreferenceCheckBox.BackColor = Color.Transparent;
                rightHandPreferenceCheckBox.BackColor = Color.Transparent;
                leftHandPreferenceCheckBox.Text = string.Format("左手");
                rightHandPreferenceCheckBox.Text = string.Format("右手");
                leftHandPreferenceCheckBox.Name = "" + k;
                rightHandPreferenceCheckBox.Name = "" + k * 2;
                rightHandPreferenceCheckBox.Checked = true;

                leftHandPreferenceCheckBox.Click += new System.EventHandler(LeftHandPreferenceCheckBox_Click);
                rightHandPreferenceCheckBox.Click += new System.EventHandler(RightHandPreferenceCheckBox_Click);

                leftHandPreferenceCheckBoxes.Insert(k, leftHandPreferenceCheckBox);
                rightHandPreferenceCheckBoxes.Insert(k, rightHandPreferenceCheckBox);


                this.Controls.Add(leftHandPreferenceCheckBox);
                this.Controls.Add(rightHandPreferenceCheckBox);
            }
        }

        public void DisposeCheckBoxes()
        {
            int checkBoxesCount = leftHandPreferenceCheckBoxes.Count;
            for (int k = 0; k < checkBoxesCount; ++k)
            {
                leftHandPreferenceCheckBoxes[k].Dispose();
                rightHandPreferenceCheckBoxes[k].Dispose();
            }
        }

        private void buttonSaveLog_Click(object sender, EventArgs e)
        {
            //setDinnerSession(defaultDinnerPeopleCount);

            //dinnerTable.OpenDinnerTable();

            //OutputLogToConsole("123");

            SaveLogToTxt();
        }

        private void outputLogButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLogToTxt();
        }

        Form settingsForm;
        TextBox peopleCountTextBox, minRandomThinkingTimeTextBox, maxRandomThinkingTimeTextBox, minRandomEatingTimeTextBox, maxRandomEatingTimeTextBox;

        private void settingsButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int currentPeopleCount = dinnerTable.GetDinnerPeopleCount();

            settingsForm = new Form();
            settingsForm.Size = new Size(370, 250);
            settingsForm.Text = "设置";
            settingsForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            settingsForm.MinimizeBox = false;
            settingsForm.MaximizeBox = false;
            settingsForm.ShowInTaskbar = false;

            Label peopleCountLabel = new Label();
            peopleCountLabel.Text = "哲学家数量:";

            peopleCountTextBox = new TextBox();
            peopleCountTextBox.Text = dinnerTable.GetDinnerPeopleCount().ToString();

            Label peopleCountTipsLabel = new Label();
            peopleCountTipsLabel.Text = "哲学家数量范围应在1~100之间";

            Label randomThinkingTimeLabel = new Label();
            randomThinkingTimeLabel.Text = "思考时间随机范围(秒):";

            Label divideLabel1 = new Label();
            divideLabel1.Text = "~";

            minRandomThinkingTimeTextBox = new TextBox();
            minRandomThinkingTimeTextBox.Text = ((double)dinnerTable.GetPhilosophersMinThinkingTime() / 1000).ToString("f1");

            maxRandomThinkingTimeTextBox = new TextBox();
            maxRandomThinkingTimeTextBox.Text = ((double)dinnerTable.GetPhilosophersMaxThinkingTime() / 1000).ToString("f1");

            Label randomEatingTimeLabel = new Label();
            randomEatingTimeLabel.Text = "进餐时间随机范围(秒):";

            Label divideLabel2 = new Label();
            divideLabel2.Text = "~";

            minRandomEatingTimeTextBox = new TextBox();
            minRandomEatingTimeTextBox.Text = ((double)dinnerTable.GetPhilosophersMinEatingTime() / 1000).ToString("f1");

            maxRandomEatingTimeTextBox = new TextBox();
            maxRandomEatingTimeTextBox.Text = ((double)dinnerTable.GetPhilosophersMaxEatingTime() / 1000).ToString("f1");

            Button okButton = new Button();
            okButton.Text = "确认";

            Button cancelButton = new Button();
            cancelButton.Text = "取消";

            peopleCountLabel.Location = new Point(30, 30);
            peopleCountLabel.Size = new Size(80, 15);
            peopleCountTextBox.Location = new Point(115, 27);
            peopleCountTextBox.Size = new Size(30, 22);
            peopleCountTipsLabel.Location = new Point(180, 30);
            peopleCountTipsLabel.Size = new Size(250, 15);

            randomThinkingTimeLabel.Location = new Point(30, 75);
            randomThinkingTimeLabel.Size = new Size(140, 15);
            divideLabel1.Location = new Point(206, 76);
            divideLabel1.Size = new Size(10, 22);
            minRandomThinkingTimeTextBox.Location = new Point(170, 72);
            minRandomThinkingTimeTextBox.Size = new Size(30, 22);
            maxRandomThinkingTimeTextBox.Location = new Point(220, 72);
            maxRandomThinkingTimeTextBox.Size = new Size(30, 22);

            randomEatingTimeLabel.Location = new Point(30, 125);
            randomEatingTimeLabel.Size = new Size(140, 15);
            divideLabel2.Location = new Point(206, 126);
            divideLabel2.Size = new Size(10, 22);
            minRandomEatingTimeTextBox.Location = new Point(170, 122);
            minRandomEatingTimeTextBox.Size = new Size(30, 22);
            maxRandomEatingTimeTextBox.Location = new Point(220, 122);
            maxRandomEatingTimeTextBox.Size = new Size(30, 22);

            okButton.Location = new Point(85, 168);
            okButton.Size = new Size(60, 25);
            cancelButton.Location = new Point(220, 168);
            cancelButton.Size = new Size(60, 25);

            okButton.Click += new System.EventHandler(SettingsFormOkButton_Click);
            cancelButton.Click += new System.EventHandler(SettingsFormCancelButton_Click);

            settingsForm.Controls.Add(peopleCountLabel);
            settingsForm.Controls.Add(peopleCountTextBox);
            settingsForm.Controls.Add(peopleCountTipsLabel);

            settingsForm.Controls.Add(randomThinkingTimeLabel);
            settingsForm.Controls.Add(divideLabel1);
            settingsForm.Controls.Add(minRandomThinkingTimeTextBox);
            settingsForm.Controls.Add(maxRandomThinkingTimeTextBox);

            settingsForm.Controls.Add(randomEatingTimeLabel);
            settingsForm.Controls.Add(divideLabel2);
            settingsForm.Controls.Add(minRandomEatingTimeTextBox);
            settingsForm.Controls.Add(maxRandomEatingTimeTextBox);

            settingsForm.Controls.Add(okButton);
            settingsForm.Controls.Add(cancelButton);




            settingsForm.ShowDialog();



        }

        private void exitButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isStartButtonSelected = true;
            startButtonToolStripMenuItem.Text = "● 开始(&I)";
            pauseButtonToolStripMenuItem.Text = "暂停(&P)";
            if (dinnerTable.GetDinnerStatusID() == 0)
            {
                dinnerTable.OpenDinnerTable();
            }
            else
            {
                dinnerTable.ResumeDinner();
            }
        }

        private void pauseButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dinnerTable.GetDinnerStatusID() == 2)
            {
                isStartButtonSelected = false;
                startButtonToolStripMenuItem.Text = "开始(&I)";
                pauseButtonToolStripMenuItem.Text = "● 暂停(&P)";
                dinnerTable.PauseDinner();
            }
        }

        private void stepIntoButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //not implemented
        }

        private void resetButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDinnerSession(originalPeopleCount);
            OutputLogToConsole("将哲学家人数重置为了" + originalPeopleCount + "人!");
        }

        private void aboutButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本程序使用.NET Framework构建\n原程序使用Java Swing构建, 本项目为Java移植", "关于");
        }


        private void LeftHandPreferenceCheckBox_Click(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int controlNumberID = int.Parse(checkBox.Name);

            dinnerTable.philosophers[controlNumberID].ChangeHandPreference(true);
            dinnerTable.philosophers[controlNumberID].ChangeHandStatus(false);

            if(rightHandPreferenceCheckBoxes[controlNumberID].Checked)
            {
                rightHandPreferenceCheckBoxes[controlNumberID].Checked = false;
            }

            if (!leftHandPreferenceCheckBoxes[controlNumberID].Checked && !rightHandPreferenceCheckBoxes[controlNumberID].Checked)
            {
                dinnerTable.philosophers[controlNumberID].ChangeHandStatus(true);
                dinnerTable.philosophers[controlNumberID].InterruptThread();
            }

        }

        private void RightHandPreferenceCheckBox_Click(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int controlNumberID = int.Parse(checkBox.Name) / 2;

            dinnerTable.philosophers[controlNumberID].ChangeHandPreference(false);
            dinnerTable.philosophers[controlNumberID].ChangeHandStatus(false);

            if (leftHandPreferenceCheckBoxes[controlNumberID].Checked)
            {
                leftHandPreferenceCheckBoxes[controlNumberID].Checked = false;
            }

            if (!leftHandPreferenceCheckBoxes[controlNumberID].Checked && !rightHandPreferenceCheckBoxes[controlNumberID].Checked)
            {
                dinnerTable.philosophers[controlNumberID].ChangeHandStatus(true);
                dinnerTable.philosophers[controlNumberID].InterruptThread();
            }
        }

        private void SettingsFormOkButton_Click(object sender, EventArgs e)
        {
            try
            {
                int peopleCount = int.Parse(peopleCountTextBox.Text);
                double minRandomThinkingTime = double.Parse(minRandomThinkingTimeTextBox.Text);
                double maxRandomThinkingTime = double.Parse(maxRandomThinkingTimeTextBox.Text);
                double minRandomEatingTime = double.Parse(minRandomEatingTimeTextBox.Text);
                double maxRandomEatingTime = double.Parse(maxRandomEatingTimeTextBox.Text);
                if (peopleCount <= 100 && peopleCount > 0)
                {
                    if (originalPeopleCount == peopleCount)
                    {
                        if (minRandomThinkingTime <= maxRandomThinkingTime && minRandomEatingTime <= maxRandomEatingTime && maxRandomThinkingTime != 0 && maxRandomEatingTime != 0)
                        {
                            SettingsConfirm((int)(minRandomThinkingTime * 1000), (int)(maxRandomThinkingTime * 1000), (int)(minRandomEatingTime * 1000), (int)(maxRandomEatingTime * 1000));
                        }
                        else
                        {
                            MessageBox.Show("输入时间范围有误!", "错误");
                            return;
                        }
                    }
                    else
                    {
                        SettingsConfirm(peopleCount, (int)(minRandomThinkingTime * 1000), (int)(maxRandomThinkingTime * 1000), (int)(minRandomEatingTime * 1000), (int)(maxRandomEatingTime * 1000));
                    }
                }
                else
                {
                    MessageBox.Show("输入的哲学家数量超出范围!", "错误");
                    return;
                }
            }
            catch(FormatException ex)
            {
                MessageBox.Show("输入格式有误!", "错误");
                return;
            }

            settingsForm.Close();
        }

        private void SettingsFormCancelButton_Click(object sender, EventArgs e)
        {
            settingsForm.Close();
        }
    }

}
