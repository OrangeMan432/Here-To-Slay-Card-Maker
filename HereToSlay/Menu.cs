using GeneratorBackend;
using HereToSlay.Properties;
using System.Diagnostics;
using System.Drawing.Text;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;

#pragma warning disable CA1416 // It onlyworks on Windows test

namespace HereToSlay
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            #region Fonts
            Font fontUI = FontLoader.GetFont("SourceSans3.ttf", 10);
            FontLoader.ChangeFontForAllControls(this, fontUI);
            selectImgButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

            Font fontUIsmall = FontLoader.GetFont("SourceSansPro.ttf", 9);
            gradient.Font = fontUIsmall;
            nameWhite.Font = fontUIsmall;
            splitClass.Font = fontUIsmall;
            gitLabel1.Font = fontUIsmall;
            gitLabel2.Font = fontUIsmall;

            Font fontLeader = FontLoader.GetFont("PatuaOne_Polish.ttf", 13);
            nameText.Font = fontLeader;
            RENDER.Font = fontLeader;

            Font fontLeaderSmall = FontLoader.GetFont("PatuaOne_Polish.ttf", 10);
            FontLoader.ChangeFontForAllLabels(this, fontLeaderSmall);
            LeaderCard.Font = fontLeaderSmall;
            MonsterCard.Font = fontLeaderSmall;
            HeroCard.Font = fontLeaderSmall;

            itemImgMore.Font = fontLeader;
            #endregion

            #region ComboBoxes
#pragma warning disable CS8622 // the warnings were annoying me, so I disabled them
            language.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            language.ComboBox.DrawItem += ImageCBox.ComboBox_DrawItem;
            language.Items.Add(new ImageCBox("English", Properties.Resources.en));
            language.Items.Add(new ImageCBox("Polski", Properties.Resources.pl));
            language.SelectedIndex = Properties.Settings.Default.Language;

            chosenClass.DrawMode = DrawMode.OwnerDrawFixed;
            chosenClass.DrawItem += ImageCBox.ComboBox_DrawItem;
            chosenClass.ItemHeight = 19;

            chosenSecondClass.DrawMode = DrawMode.OwnerDrawFixed;
            chosenSecondClass.DrawItem += ImageCBox.ComboBox_DrawItem;
            chosenSecondClass.ItemHeight = 19;

            heroReq1.DrawMode = DrawMode.OwnerDrawFixed;
            heroReq1.DrawItem += ImageCBox.ComboBox_DrawItem;
            heroReq1.ItemHeight = 19;
            heroReq2.DrawMode = DrawMode.OwnerDrawFixed;
            heroReq2.DrawItem += ImageCBox.ComboBox_DrawItem;
            heroReq2.ItemHeight = 19;
            heroReq3.DrawMode = DrawMode.OwnerDrawFixed;
            heroReq3.DrawItem += ImageCBox.ComboBox_DrawItem;
            heroReq3.ItemHeight = 19;
            heroReq4.DrawMode = DrawMode.OwnerDrawFixed;
            heroReq4.DrawItem += ImageCBox.ComboBox_DrawItem;
            heroReq4.ItemHeight = 19;
            heroReq5.DrawMode = DrawMode.OwnerDrawFixed;
            heroReq5.DrawItem += ImageCBox.ComboBox_DrawItem;
            heroReq5.ItemHeight = 19;

            badOutputSym.DrawMode = DrawMode.OwnerDrawFixed;
            badOutputSym.DrawItem += BackgroundCBox.ComboBox_DrawItem;
            badOutputSym.ItemHeight = 19;
            badOutputSym.Items.Add(new BackgroundCBox("+", Color.FromArgb(109, 166, 88)));
            badOutputSym.Items.Add(new BackgroundCBox("-", Color.FromArgb(230, 44, 47)));
            badOutputSym.ItemHeight = 17;

            goodOutputSym.DrawMode = DrawMode.OwnerDrawFixed;
            goodOutputSym.DrawItem += BackgroundCBox.ComboBox_DrawItem;
            goodOutputSym.ItemHeight = 19;
            goodOutputSym.Items.Add(new BackgroundCBox("+", Color.FromArgb(109, 166, 88)));
            goodOutputSym.Items.Add(new BackgroundCBox("-", Color.FromArgb(230, 44, 47)));
            goodOutputSym.ItemHeight = 17;

            badOutputSym.SelectedIndex = 1;
            goodOutputSym.SelectedIndex = 0;

#pragma warning restore CS8622
            #endregion

            switch (Properties.Settings.Default.CardType)
            {
                case 0:
                    LeaderCard_Click(null, null);
                    break;
                case 1:
                    MonsterCard_Click(null, null);
                    break;
                case 2:
                    HeroCard_Click(null, null);
                    break;
            }
        }

        // TODO: make it one method
        // To anyone trying to understand the code below: I'm sorry, I'm not proud of it either. The lazynies is too great.
        #region Card Type Selection 
        private void LeaderCard_Click(object? sender, EventArgs? e)
        {
            if (LeaderCard.Checked == false)
            {
                Properties.Settings.Default.CardType = 0;
                Properties.Settings.Default.Save();

                if (chosenClass.SelectedIndex == -1)
                {
                    this.Icon = Properties.Resources.LEADER;
                }

                MonsterCard.Checked = false;
                MonsterCard.BackColor = SystemColors.Control;
                LeaderCard.Checked = true;
                LeaderCard.BackColor = SystemColors.ControlDark;
                HeroCard.Checked = false;
                HeroCard.BackColor = SystemColors.Control;

                chosenClass.Visible = true;
                labelClass.Visible = true;
                advancedClass.Visible = true;
                advancedClass.Image = Properties.Resources.closed;
                splitClass.Checked = false;

                advancedName.Visible = true;

                labelImg.Location = new Point(labelImg.Location.X, 329);
                selectImgText.Location = new Point(selectImgText.Location.X, 349);
                selectImgButton.Location = new Point(selectImgButton.Location.X, 349);

                RENDER.Location = new Point(RENDER.Location.X, 525);

                labelReq.Visible = false;
                heroReq1.Visible = false;
                heroReq2.Visible = false;
                heroReq3.Visible = false;
                heroReq4.Visible = false;
                heroReq5.Visible = false;

                labelBad.Visible = false;
                badOutputText.Visible = false;
                badOutputNum.Visible = false;
                badOutputSym.Visible = false;

                labelGood.Visible = false;
                goodOutputText.Visible = false;
                goodOutputNum.Visible = false;
                goodOutputSym.Visible = false;

                maxItems.Visible = false;
                labelMaxItem.Visible = false;
                itemImg.Visible = false;
                itemImg2.Visible = false;
                itemImgMore.Visible = false;

                descriptionText.Size = new Size(300, descriptionText.Size.Height);
                labelDescription.Location = new Point(53, 426);
                descriptionText.Location = new Point(57, 446);

                foreach (Control c in this.Controls)
                {
                    if (c.Name.Contains("clear")) { c.Visible = false; };
                }

                nameWhite.Checked = false;

                this.Update();
                updateLanguage(sender, e);
                this.Update();

                renderPreviewNow(sender, e);
                previewImg.ImageLocation = Path.Combine(Directory.GetCurrentDirectory(), "preview.png");
                previewImg.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void MonsterCard_Click(object? sender, EventArgs? e)
        {
            if (MonsterCard.Checked == false)
            {
                Properties.Settings.Default.CardType = 1;
                Properties.Settings.Default.Save();

                this.Icon = Properties.Resources.monster;

                LeaderCard.Checked = false;
                LeaderCard.BackColor = SystemColors.Control;
                MonsterCard.Checked = true;
                MonsterCard.BackColor = SystemColors.ControlDark;
                HeroCard.Checked = false;
                HeroCard.BackColor = SystemColors.Control;

                chosenClass.Visible = false;
                labelClass.Visible = false;
                chosenSecondClass.Visible = false;
                clearSecondClass.Visible = false;
                labelSecondClass.Visible = false;
                advancedClass.Visible = false;
                advancedClassBox.Visible = false;

                advancedName.Visible = true;

                labelImg.Location = new Point(labelImg.Location.X, 229);
                selectImgText.Location = new Point(selectImgText.Location.X, 249);
                selectImgButton.Location = new Point(selectImgButton.Location.X, 249);

                RENDER.Location = new Point(RENDER.Location.X, 591);

                labelReq.Visible = true;
                heroReq1.Visible = true;
                heroReq2.Visible = true;
                heroReq3.Visible = true;
                heroReq4.Visible = true;
                heroReq5.Visible = true;

                labelBad.Visible = true;
                badOutputText.Visible = true;
                badOutputNum.Visible = true;
                badOutputSym.Visible = true;

                labelGood.Visible = true;
                goodOutputText.Visible = true;

                goodOutputNum.Visible = true;
                goodOutputSym.Visible = true;
                goodOutputNum.Location = new Point(84, 457);
                goodOutputSym.Location = new Point(118, 457);

                maxItems.Visible = false;
                labelMaxItem.Visible = false;
                itemImg.Visible = false;
                itemImg2.Visible = false;
                itemImgMore.Visible = false;

                descriptionText.Size = new Size(300, descriptionText.Size.Height);
                labelDescription.Location = new Point(53, 492);
                descriptionText.Location = new Point(57, 512);

                foreach (Control c in this.Controls)
                {
                    if (c.Name.Contains("clear") && c.Name != "clearSecondClass") { c.Visible = true; };
                }

                nameWhite.Checked = true;

                this.Update();
                updateLanguage(sender, e);
                this.Update();

                renderPreviewNow(sender, e);
                previewImg.ImageLocation = Path.Combine(Directory.GetCurrentDirectory(), "preview.png");
                previewImg.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void HeroCard_Click(object? sender, EventArgs? e)
        {
            if (HeroCard.Checked == false)
            {
                Properties.Settings.Default.CardType = 2;
                Properties.Settings.Default.Save();

                if (chosenClass.SelectedIndex == -1)
                {
                    this.Icon = Properties.Settings.Default.Language switch
                    {
                        1 => Properties.Resources.bohater,
                        _ => Properties.Resources.hero
                    };
                }

                LeaderCard.Checked = false;
                LeaderCard.BackColor = SystemColors.Control;
                MonsterCard.Checked = false;
                MonsterCard.BackColor = SystemColors.Control;
                HeroCard.Checked = true;
                HeroCard.BackColor = SystemColors.ControlDark;

                chosenClass.Visible = true;
                labelClass.Visible = true;
                advancedClass.Visible = false;
                advancedClass.Image = Properties.Resources.closed;
                splitClass.Checked = false;
                advancedClassBox.Visible = false;

                advancedName.Visible = false;
                advancedName.Image = Properties.Resources.closed;
                advancedNameBox.Visible = false;

                labelImg.Location = new Point(labelImg.Location.X, 304);
                selectImgText.Location = new Point(selectImgText.Location.X, 324);
                selectImgButton.Location = new Point(selectImgButton.Location.X, 324);

                RENDER.Location = new Point(RENDER.Location.X, 525);

                labelReq.Visible = false;
                heroReq1.Visible = false;
                heroReq2.Visible = false;
                heroReq3.Visible = false;
                heroReq4.Visible = false;
                heroReq5.Visible = false;

                labelBad.Visible = false;
                badOutputText.Visible = false;
                badOutputNum.Visible = false;
                badOutputSym.Visible = false;

                labelGood.Visible = false;
                goodOutputText.Visible = false;

                goodOutputNum.Visible = true;
                goodOutputSym.Visible = true;
                goodOutputNum.Location = new Point(57, 466);
                goodOutputSym.Location = new Point(92, 466);

                maxItems.Visible = true;
                labelMaxItem.Visible = true;
                itemImg.Visible = true;

                descriptionText.Size = new Size(225, descriptionText.Size.Height);
                labelDescription.Location = new Point(128, 426);
                descriptionText.Location = new Point(132, 446);

                foreach (Control c in this.Controls)
                {
                    if (c.Name.Contains("clear")) { c.Visible = false; };
                }

                this.Update();
                updateLanguage(sender, e);
                this.Update();

                renderPreviewNow(sender, e);
                previewImg.ImageLocation = Path.Combine(Directory.GetCurrentDirectory(), "preview.png");
                previewImg.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        #endregion

        #region Direct Rendering (Preview and Save)
        private void RenderButton_Press(object sender, EventArgs e)
        {
            using SaveFileDialog SaveRenderDialog = new();
            SaveRenderDialog.Filter = "Supported Image Files (*.png;)|*.png;|All Files (*.*)|*.*";
            SaveRenderDialog.FilterIndex = 1;

            if (SaveRenderDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = SaveRenderDialog.FileName;
                switch (Properties.Settings.Default.CardType)
                {
                    case 0:
                        GeneratorBackend.Program.GenerateLeader(filePath, language.SelectedIndex, nameText.Text, new int[] { chosenClass.SelectedIndex, chosenSecondClass.SelectedIndex }, selectImgText.Text, descriptionText.Text, gradient.Checked, nameWhite.Checked);
                        break;
                    case 1:
                        RollOutput good = new((int)goodOutputNum.Value, goodOutputSym.SelectedIndex, goodOutputText.Text);
                        RollOutput bad = new((int)badOutputNum.Value, badOutputSym.SelectedIndex, badOutputText.Text);
                        int[] desiredRequirements = new int[] { heroReq1.SelectedIndex, heroReq2.SelectedIndex, heroReq3.SelectedIndex, heroReq4.SelectedIndex, heroReq5.SelectedIndex };
                        GeneratorBackend.Program.GenerateMonster(filePath, language.SelectedIndex, nameText.Text, desiredRequirements, good, bad, selectImgText.Text, descriptionText.Text, gradient.Checked, nameWhite.Checked);
                        break;
                    case 2:
                        RollOutput description = new((int)goodOutputNum.Value, goodOutputSym.SelectedIndex, descriptionText.Text);
                        GeneratorBackend.Program.GenerateHero(filePath, language.SelectedIndex, nameText.Text, chosenClass.SelectedIndex, selectImgText.Text, description, (int)maxItems.Value);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                previewImg.ImageLocation = filePath;
            }
        }

        private CancellationTokenSource? cancellationTokenSource;
        private async void renderPreview(object? sender, EventArgs? e)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            try
            {
                int timer = 1;
                while (timer > 0)
                {
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    await Task.Delay(200, cancellationTokenSource.Token);
                    timer--;
                }
                if (timer >= 0)
                {
                    renderPreviewNow(sender, e);
                    previewImg.ImageLocation = Path.Combine(Directory.GetCurrentDirectory(), "preview.png"); ;
                }
            }
            catch (OperationCanceledException) { }
        }

        private void renderPreviewNow(object? sender, EventArgs? e)
        {
            switch (Properties.Settings.Default.CardType)
            {
                case 0:
                    GeneratorBackend.Program.GenerateLeader(null, language.SelectedIndex, nameText.Text, new int[] { chosenClass.SelectedIndex, chosenSecondClass.SelectedIndex }, selectImgText.Text, descriptionText.Text, gradient.Checked, nameWhite.Checked);
                    break;
                case 1:
                    RollOutput good = new((int)goodOutputNum.Value, goodOutputSym.SelectedIndex, goodOutputText.Text);
                    RollOutput bad = new((int)badOutputNum.Value, badOutputSym.SelectedIndex, badOutputText.Text);
                    int[] desiredRequirements = new int[] { heroReq1.SelectedIndex, heroReq2.SelectedIndex, heroReq3.SelectedIndex, heroReq4.SelectedIndex, heroReq5.SelectedIndex };
                    GeneratorBackend.Program.GenerateMonster(null, language.SelectedIndex, nameText.Text, desiredRequirements, good, bad, selectImgText.Text, descriptionText.Text, gradient.Checked, nameWhite.Checked);
                    break;
                case 2:
                    RollOutput description = new((int)goodOutputNum.Value, goodOutputSym.SelectedIndex, descriptionText.Text);
                    GeneratorBackend.Program.GenerateHero(null, language.SelectedIndex, nameText.Text, chosenClass.SelectedIndex, selectImgText.Text, description, (int)maxItems.Value);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion

        #region Language
        private void language_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateLanguage(sender, e);
            renderPreview(sender, e);
        }

        private void updateLanguage(object? sender, EventArgs? e)
        {
            Properties.Settings.Default.Language = language.SelectedIndex;
            Properties.Settings.Default.Save();

            switch (language.SelectedIndex)
            {
                case 1:
                    logo.Image = Properties.Resources.Logo1;
                    labelLeader.Text = Properties.Settings.Default.CardType switch
                    {
                        1 => "Nazwa potwora",
                        2 => "Nazwa bohatera",
                        _ => "Nazwa przyw�dcy"
                    };
                    labelClass.Text = "Klasa przyw�dcy";
                    labelSecondClass.Text = "Druga klasa";
                    labelImg.Text = Properties.Settings.Default.CardType switch
                    {
                        1 => "Obrazek potwora",
                        2 => "Obrazek bohatera",
                        _ => "Obrazek przyw�dcy"
                    };
                    labelDescription.Text = "Opis mocy";
                    leaderImgToolTip.ToolTipTitle = "Wymiary obazka";
                    leaderImgToolTip.SetToolTip(selectImgButton, Properties.Settings.Default.CardType switch
                    {
                        1 => "Obrazek potwora (nie ca�a karta) ma wymiary 745x817. \nProgram automatycznie przytnie i przybli�y obraz, je�eli b�dzie to potrzebne.\n\nWspierane rozszerzenia plik�w:\n.png, .jpeg, .jpg, .gif (pierwsza klatka), .bmp, .webp, .pbm, .tiff, .tga",
                        2 => "#TODO",
                        _ => "Obrazek przyw�dcy (nie ca�a karta) ma wymiary 745x1176. \nProgram automatycznie przytnie i przybli�y obraz, je�eli b�dzie to potrzebne.\n\nWspierane rozszerzenia plik�w:\n.png, .jpeg, .jpg, .gif (pierwsza klatka), .bmp, .webp, .pbm, .tiff, .tga"
                    });
                    gradient.Text = "Tylni gradient";
                    nameWhite.Text = "Bia�a nazwa";
                    splitClass.Text = "Podw�jna Klasa";
                    this.Text = "To ja go tn� - Generator kart";
                    labelBad.Text = "Wymagania rzutu - Pora�ka";
                    labelGood.Text = "Wymagania rzutu - UBIJ potwora";
                    goodOutputText.Text = "UBIJ tego potwora";
                    labelReq.Text = "Wymagania bohater�w";

                    if (chosenClass.SelectedIndex == -1 && Properties.Settings.Default.CardType == 2)
                    {
                        this.Icon = Properties.Resources.bohater;
                    }

                    break;

                default:
                    logo.Image = Properties.Resources.Logo0;
                    labelLeader.Text = Properties.Settings.Default.CardType switch
                    {
                        1 => "Monster name",
                        2 => "Hero name",
                        _ => "Leader name"
                    };
                    labelClass.Text = "Leader class";
                    labelSecondClass.Text = "Second class";
                    labelImg.Text = Properties.Settings.Default.CardType switch
                    {
                        1 => "Monster image",
                        2 => "Hero image",
                        _ => "Leader image"
                    };
                    labelDescription.Text = "Description";
                    leaderImgToolTip.ToolTipTitle = "Image dimentions";
                    leaderImgToolTip.SetToolTip(selectImgButton, Properties.Settings.Default.CardType switch
                    {
                        1 => "The monster image (not the whole card) dimentions are 745x817. \nThe program will automatically crop and zoom the image, if needed.\n\nSupported file extensions:\n.png, .jpeg, .jpg, .gif (first frame), .bmp, .webp, .pbm, .tiff, .tga",
                        2 => "#TODO",
                        _ => "The leader image (not the whole card) dimentions are 745x1176. \nThe program will automatically crop and zoom the image, if needed.\n\nSupported file extensions:\n.png, .jpeg, .jpg, .gif (first frame), .bmp, .webp, .pbm, .tiff, .tga"
                    });
                    gradient.Text = "Back gradient";
                    nameWhite.Text = "White name";
                    splitClass.Text = "Split Class";
                    this.Text = "Here to Slay - Card generator";
                    labelBad.Text = "Roll Requirements - Fail";
                    labelGood.Text = "Roll Requirements - SLAY monster";
                    goodOutputText.Text = "SLAY this Monster card";
                    labelReq.Text = "Hero Requirements";

                    if (chosenClass.SelectedIndex == -1 && Properties.Settings.Default.CardType == 2)
                    {
                        this.Icon = Properties.Resources.hero;
                    }

                    break;
            }
            LocaliseClassOptions(language.SelectedIndex); // change class options based on selected language
        }

        // These are made so that after changing the language, the selected classes stay the same.
        int currentClassIndex;
        int currentSecondClassIndex;
        int currentHeroReq1Index;
        int currentHeroReq2Index;
        int currentHeroReq3Index;
        int currentHeroReq4Index;
        int currentHeroReq5Index;
        private void LocaliseClassOptions(int lang)
        {
            currentClassIndex = chosenClass.SelectedIndex;
            chosenClass.Items.Clear();

            currentSecondClassIndex = chosenSecondClass.SelectedIndex;
            chosenSecondClass.Items.Clear();

            currentHeroReq1Index = heroReq1.SelectedIndex;
            heroReq1.Items.Clear();
            currentHeroReq2Index = heroReq2.SelectedIndex;
            heroReq2.Items.Clear();
            currentHeroReq3Index = heroReq3.SelectedIndex;
            heroReq3.Items.Clear();
            currentHeroReq4Index = heroReq4.SelectedIndex;
            heroReq4.Items.Clear();
            currentHeroReq5Index = heroReq5.SelectedIndex;
            heroReq5.Items.Clear();

            switch (lang)
            {
                case 1:
                    heroReq1.Items.Add(new ImageCBox("BOHATER", Properties.Resources.bohater.ToBitmap()));
                    heroReq2.Items.Add(new ImageCBox("BOHATER", Properties.Resources.bohater.ToBitmap()));
                    heroReq3.Items.Add(new ImageCBox("BOHATER", Properties.Resources.bohater.ToBitmap()));
                    heroReq4.Items.Add(new ImageCBox("BOHATER", Properties.Resources.bohater.ToBitmap()));
                    heroReq5.Items.Add(new ImageCBox("BOHATER", Properties.Resources.bohater.ToBitmap()));

                    chosenClass.Items.Add(new ImageCBox("�owca", Properties.Resources.lowca.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Mag", Properties.Resources.mag.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Bard", Properties.Resources.najebus.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Stra�nik", Properties.Resources.straznik.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Wojownik", Properties.Resources.wojownik.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Z�odziej", Properties.Resources.zlodziej.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Druid", Properties.Resources.druid.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Awanturnik", Properties.Resources.awanturnik.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Berserk", Properties.Resources.berserk.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Nekromanta", Properties.Resources.nekromanta.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Czarownik", Properties.Resources.czarownik.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("BRAK KLASY", Properties.Resources.empty.ToBitmap()));
                    break;
                default:
                    heroReq1.Items.Add(new ImageCBox("HERO", Properties.Resources.hero.ToBitmap()));
                    heroReq2.Items.Add(new ImageCBox("HERO", Properties.Resources.hero.ToBitmap()));
                    heroReq3.Items.Add(new ImageCBox("HERO", Properties.Resources.hero.ToBitmap()));
                    heroReq4.Items.Add(new ImageCBox("HERO", Properties.Resources.hero.ToBitmap()));
                    heroReq5.Items.Add(new ImageCBox("HERO", Properties.Resources.hero.ToBitmap()));

                    chosenClass.Items.Add(new ImageCBox("Ranger", Properties.Resources.lowca.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Wizard", Properties.Resources.mag.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Bard", Properties.Resources.najebus.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Guardian", Properties.Resources.straznik.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Fighter", Properties.Resources.wojownik.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Thief", Properties.Resources.zlodziej.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Druid", Properties.Resources.druid.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Warrior", Properties.Resources.awanturnik.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Berserker", Properties.Resources.berserk.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Necromancer", Properties.Resources.nekromanta.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("Sorcerer", Properties.Resources.czarownik.ToBitmap()));
                    chosenClass.Items.Add(new ImageCBox("NO CLASS", Properties.Resources.empty.ToBitmap()));
                    break;
            }

            foreach (var item in chosenClass.Items)
            {
                chosenSecondClass.Items.Add(item);
                heroReq1.Items.Add(item);
                heroReq2.Items.Add(item);
                heroReq3.Items.Add(item);
                heroReq4.Items.Add(item);
                heroReq5.Items.Add(item);
            }

            chosenClass.SelectedIndex = currentClassIndex;
            chosenSecondClass.SelectedIndex = currentSecondClassIndex;

            heroReq1.SelectedIndex = currentHeroReq1Index;
            heroReq2.SelectedIndex = currentHeroReq2Index;
            heroReq3.SelectedIndex = currentHeroReq3Index;
            heroReq4.SelectedIndex = currentHeroReq4Index;
            heroReq5.SelectedIndex = currentHeroReq5Index;
        }
        #endregion

        #region Class Related Methods
        private void chosenClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateIcon_to_chosenClass(sender, e);
            renderPreview(sender, e);
        }

        private void updateIcon_to_chosenClass(object? sender, EventArgs? e)
        {
            if (Properties.Settings.Default.CardType == 0 || Properties.Settings.Default.CardType == 2)
            {
                this.Icon = chosenClass.SelectedIndex switch
                {
                    0 => Properties.Resources.lowca,
                    1 => Properties.Resources.mag,
                    2 => Properties.Resources.najebus,
                    3 => Properties.Resources.straznik,
                    4 => Properties.Resources.wojownik,
                    5 => Properties.Resources.zlodziej,
                    6 => Properties.Resources.druid,
                    7 => Properties.Resources.awanturnik,
                    8 => Properties.Resources.berserk,
                    9 => Properties.Resources.nekromanta,
                    10 => Properties.Resources.czarownik,
                    11 => Properties.Resources.empty,
                    _ => Properties.Resources.LEADER
                };
            }
        }

        private void splitClass_CheckStateChanged(object sender, EventArgs e)
        {
            switch (splitClass.Checked)
            {
                case false:
                    chosenSecondClass.Visible = false;
                    labelSecondClass.Visible = false;
                    clearSecondClass.Visible = false;
                    chosenClass.Location = new Point(chosenClass.Location.X + 63, chosenClass.Location.Y);
                    labelClass.Location = new Point(labelClass.Location.X + 63, labelClass.Location.Y);
                    chosenSecondClass.SelectedIndex = -1;
                    break;
                case true:
                    chosenClass.Location = new Point(chosenClass.Location.X - 63, chosenClass.Location.Y);
                    labelClass.Location = new Point(labelClass.Location.X - 63, labelClass.Location.Y);
                    chosenSecondClass.Visible = true;
                    labelSecondClass.Visible = true;
                    clearSecondClass.Visible = true;
                    break;
            }
        }

        private void clearSelectedClass(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Name)
                {
                    case "clearSecondClass":
                        chosenSecondClass.SelectedIndex = -1;
                        break;
                    case "clearHeroReq1":
                        heroReq1.SelectedIndex = -1;
                        break;
                    case "clearHeroReq2":
                        heroReq2.SelectedIndex = -1;
                        break;
                    case "clearHeroReq3":
                        heroReq3.SelectedIndex = -1;
                        break;
                    case "clearHeroReq4":
                        heroReq4.SelectedIndex = -1;
                        break;
                    case "clearHeroReq5":
                        heroReq5.SelectedIndex = -1;
                        break;
                }
            }
        }
        #endregion

        #region UI Elements
        private void advanced_Click(object sender, EventArgs e)
        {
            PictureBox? pictureBox = sender as PictureBox ?? throw new NotImplementedException();
            FlowLayoutPanel list = pictureBox.Name switch
            {
                "advancedName" => advancedNameBox,
                "advancedClass" => advancedClassBox,
                _ => throw new NotImplementedException(),
            };
            list.Visible = !list.Visible;
            pictureBox.Image = list.Visible switch
            {
                true => Properties.Resources.open,
                false => Properties.Resources.closed
            };
        }

        private void selectImg_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Title = "Select a File";
            openFileDialog.Filter = "Supported Image Files (*.png;*.jpeg;*.jpg;*.gif;*.bmp;*.webp;*.pbm;*.tiff;*.tga;)|*.png;*.jpeg;*.jpg;*.gif;*.bmp;*.webp;*.pbm;*.tiff;*.tga;|All Files (*.*)|*.*"; ;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                selectImgText.Text = selectedFilePath;
            }
        }

        private void previewImg_Click(object sender, EventArgs e)
        {
            if (previewImg != null)
            {
                string previewImgPath = previewImg.ImageLocation;
                string? folderPath = Path.GetDirectoryName(previewImgPath);
                if (folderPath != null)
                {
                    Process.Start("explorer.exe", folderPath);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = "https://github.com/Danrejk", UseShellExecute = true }); }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = "https://github.com/Beukot", UseShellExecute = true }); }

        private void copyImageToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(previewImg.Image);
        }
        #endregion

        private void OutputSym_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                comboBox.BackColor = comboBox.SelectedIndex switch
                {
                    0 => Color.FromArgb(109, 166, 88),
                    1 => Color.FromArgb(230, 44, 47),
                    _ => throw new NotImplementedException(),
                };

                switch (comboBox.Name)
                {
                    case "goodOutputSym":
                        badOutputSym.SelectedIndex = goodOutputSym.SelectedIndex switch
                        {
                            0 => 1,
                            _ => 0,
                        };
                        break;
                    case "badOutputSym":
                        goodOutputSym.SelectedIndex = badOutputSym.SelectedIndex switch
                        {
                            0 => 1,
                            _ => 0,
                        };
                        break;
                }
            }
            renderPreview(sender, e);
        }

        private void maxItems_ValueChanged(object sender, EventArgs e)
        {
            switch (maxItems.Value)
            {
                case 0:
                    itemImg.Image = Properties.Resources.noItem;
                    itemImg2.Visible = false;
                    itemImgMore.Visible = false;
                    break;
                case 1:
                    itemImg.Image = Properties.Resources.item;
                    itemImg2.Visible = false;
                    itemImgMore.Visible = false;
                    break;
                case 2:
                    itemImg.Image = Properties.Resources.item;
                    itemImg2.Visible = true;
                    itemImgMore.Visible = false;
                    break;
                default:
                    itemImg.Image = Properties.Resources.item;
                    itemImg2.Visible = true;
                    itemImgMore.Visible = true;
                    break;
            }
            itemImg.Image = maxItems.Value switch
            {
                0 => Properties.Resources.noItem,
                _ => Properties.Resources.item,
            };

            renderPreview(sender, e);
        }
    }

    class FontLoader
    {
        private static readonly PrivateFontCollection FontCollection = new PrivateFontCollection();

        public static Font GetFont(string fontFileName, float size)
        {
            string executableLocation = AppDomain.CurrentDomain.BaseDirectory;
            string fontPath = Path.Combine(executableLocation, "Fonts", fontFileName);
            if (!File.Exists(fontPath))
            {
                throw new FileNotFoundException("Font file not found.");
            }

            FontCollection.AddFontFile(fontPath);
            FontFamily fontFamily = FontCollection.Families[0];

            Font font = new(fontFamily, size);
            return font;
        }

        public static void ChangeFontForAllControls(Control control, Font fontUI)
        {
            foreach (Control c in control.Controls)
            {
                if (!c.Name.Contains("clear") && c is not NumericUpDown) // don't change the font for the X boxes or the Numeric boxes
                {
                    c.Font = fontUI;
                    if (c.Controls.Count > 0)
                    {
                        ChangeFontForAllControls(c, fontUI);
                    }
                }
            }
        }

        public static void ChangeFontForAllLabels(Control control, Font fontUI)
        {
            foreach (Control c in control.Controls)
            {
                if (c is Label label)
                {
                    label.Font = fontUI;
                }
                if (c.Controls.Count > 0)
                {
                    ChangeFontForAllLabels(c, fontUI);
                }
            }
        }
    }

    class ImageCBox
    {
        public string Text { get; set; }
        public Image Image { get; set; }

        public ImageCBox(string text, Image image)
        {
            Text = text;
            Image = image;
        }

        public static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || sender == null) return;

            e.DrawBackground();

            if (sender is ComboBox comboBox && comboBox.Items.Count > 0 && comboBox.Items[e.Index] is ImageCBox item)
            {
                if (e.Font != null)
                {
                    e.Graphics.DrawImage(item.Image, e.Bounds.Left, e.Bounds.Top, e.Bounds.Height, e.Bounds.Height);
                    e.Graphics.DrawString(item.Text, e.Font, Brushes.Black, e.Bounds.Left + e.Bounds.Height, e.Bounds.Top);
                }
            }

            e.DrawFocusRectangle();
        }
    }
    class BackgroundCBox
    {
        public string Text { get; set; }
        public Color BackgroundColor { get; set; }

        public BackgroundCBox(string text, Color backgroundColor)
        {
            Text = text;
            BackgroundColor = backgroundColor;
        }

        public static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || sender == null) return;

            e.DrawBackground();

            if (sender is ComboBox comboBox && comboBox.Items.Count > 0 && comboBox.Items[e.Index] is BackgroundCBox item)
            {
                if (e.Font != null)
                {
                    Brush backgroundColorBrush = new SolidBrush(item.BackgroundColor);
                    e.Graphics.FillRectangle(backgroundColorBrush, e.Bounds);

                    StringFormat stringFormat = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    };

                    Font customFont = FontLoader.GetFont("PatuaOne_Polish.ttf", 14);
                    Brush textColorBrush = Brushes.White;
                    e.Graphics.DrawString(item.Text, customFont, textColorBrush, e.Bounds, stringFormat);
                }
            }

            e.DrawFocusRectangle();
        }
    }


}