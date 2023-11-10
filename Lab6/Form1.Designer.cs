namespace Lab6
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            Box = new PictureBox();
            PerspectiveComboBox = new ComboBox();
            ApplyPerspective = new Button();
            PrimitiveComboBox = new ComboBox();
            PerspectiveLabel = new Label();
            PrimitiveLabel = new Label();
            ApplyPrimitive = new Button();
            Translate1 = new NumericUpDown();
            Translate2 = new NumericUpDown();
            Translate3 = new NumericUpDown();
            XLable = new Label();
            YLable = new Label();
            ZLable = new Label();
            OffsetLable = new Label();
            Rotate1 = new NumericUpDown();
            Rotate2 = new NumericUpDown();
            Rotate3 = new NumericUpDown();
            Scale1 = new NumericUpDown();
            Scale2 = new NumericUpDown();
            Scale3 = new NumericUpDown();
            RotateLable = new Label();
            ScaleLable = new Label();
            ApplyAffin = new Button();
            ReflectionComboBox = new ComboBox();
            ApplyReflection = new Button();
            Reflectionlable = new Label();
            GeneratrixX = new NumericUpDown();
            GeneratrixY = new NumericUpDown();
            GeneratrixZ = new NumericUpDown();
            Point2X = new NumericUpDown();
            Point2Y = new NumericUpDown();
            Point2Z = new NumericUpDown();
            RotateAroundLineLable = new Label();
            Angle = new NumericUpDown();
            ApplyLineRotation = new Button();
            AngleLabel = new Label();
            Point1Lable = new Label();
            Point2Lable = new Label();
            SaveButton = new Button();
            UploadButton = new Button();
            GeneratrixListBox = new ListBox();
            GeneratrixLabel = new Label();
            AddPointGeneratrix = new Button();
            DeletePointGeneratrix = new Button();
            PartitionLabel = new Label();
            Partition = new NumericUpDown();
            Build = new Button();
            Point1X = new NumericUpDown();
            Point1Y = new NumericUpDown();
            Point1Z = new NumericUpDown();
            AxisLabel = new Label();
            AxisComboBox = new ComboBox();
            FunctionTextBox = new TextBox();
            FunctionLabel = new Label();
            FunctionX0UpDown = new NumericUpDown();
            FunctionX1UpDown = new NumericUpDown();
            FunctionY0UpDown = new NumericUpDown();
            FunctionY1UpDown = new NumericUpDown();
            FunctionX0Label = new Label();
            FunctionX1Label = new Label();
            FunctionY0Label = new Label();
            FunctionY1Label = new Label();
            FunctionXStepUpDown = new NumericUpDown();
            FunctionYStepUpDown = new NumericUpDown();
            FunctionXStepLabel = new Label();
            FunctionYStepLabel = new Label();
            FunctionBuild = new Button();
            ((System.ComponentModel.ISupportInitialize)Box).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Translate1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Translate2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Translate3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Rotate1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Rotate2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Rotate3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Scale1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Scale2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Scale3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GeneratrixX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GeneratrixY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GeneratrixZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Point2X).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Point2Y).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Point2Z).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Angle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Partition).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Point1X).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Point1Y).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Point1Z).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FunctionX0UpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FunctionX1UpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FunctionY0UpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FunctionY1UpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FunctionXStepUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FunctionYStepUpDown).BeginInit();
            SuspendLayout();
            // 
            // Box
            // 
            Box.BackColor = SystemColors.ControlLightLight;
            Box.BorderStyle = BorderStyle.FixedSingle;
            Box.Location = new System.Drawing.Point(12, 12);
            Box.Name = "Box";
            Box.Size = new Size(948, 599);
            Box.TabIndex = 0;
            Box.TabStop = false;
            // 
            // PerspectiveComboBox
            // 
            PerspectiveComboBox.FormattingEnabled = true;
            PerspectiveComboBox.Items.AddRange(new object[] { "Perspective", "Isometric", "Orthographic XY", "Orthographic XZ", "Orthographic YZ" });
            PerspectiveComboBox.Location = new System.Drawing.Point(970, 34);
            PerspectiveComboBox.Name = "PerspectiveComboBox";
            PerspectiveComboBox.Size = new Size(190, 27);
            PerspectiveComboBox.TabIndex = 2;
            // 
            // ApplyPerspective
            // 
            ApplyPerspective.Location = new System.Drawing.Point(970, 67);
            ApplyPerspective.Name = "ApplyPerspective";
            ApplyPerspective.Size = new Size(190, 34);
            ApplyPerspective.TabIndex = 4;
            ApplyPerspective.Text = "Apply";
            ApplyPerspective.UseVisualStyleBackColor = true;
            ApplyPerspective.Click += ApplyPerspective_Click;
            // 
            // PrimitiveComboBox
            // 
            PrimitiveComboBox.FormattingEnabled = true;
            PrimitiveComboBox.Items.AddRange(new object[] { "Tetrahedron", "Hexahedron", "Octahedron" });
            PrimitiveComboBox.Location = new System.Drawing.Point(970, 130);
            PrimitiveComboBox.Name = "PrimitiveComboBox";
            PrimitiveComboBox.Size = new Size(190, 27);
            PrimitiveComboBox.TabIndex = 6;
            // 
            // PerspectiveLabel
            // 
            PerspectiveLabel.AutoSize = true;
            PerspectiveLabel.Location = new System.Drawing.Point(966, 12);
            PerspectiveLabel.Name = "PerspectiveLabel";
            PerspectiveLabel.Size = new Size(121, 19);
            PerspectiveLabel.TabIndex = 7;
            PerspectiveLabel.Text = "Choose projection";
            // 
            // PrimitiveLabel
            // 
            PrimitiveLabel.AutoSize = true;
            PrimitiveLabel.Location = new System.Drawing.Point(966, 106);
            PrimitiveLabel.Name = "PrimitiveLabel";
            PrimitiveLabel.Size = new Size(78, 19);
            PrimitiveLabel.TabIndex = 9;
            PrimitiveLabel.Text = "Polyhedron";
            // 
            // ApplyPrimitive
            // 
            ApplyPrimitive.Location = new System.Drawing.Point(970, 163);
            ApplyPrimitive.Name = "ApplyPrimitive";
            ApplyPrimitive.Size = new Size(190, 34);
            ApplyPrimitive.TabIndex = 10;
            ApplyPrimitive.Text = "Apply";
            ApplyPrimitive.UseVisualStyleBackColor = true;
            ApplyPrimitive.Click += ApplyPrimitive_Click;
            // 
            // Translate1
            // 
            Translate1.DecimalPlaces = 2;
            Translate1.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            Translate1.Location = new System.Drawing.Point(970, 225);
            Translate1.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            Translate1.Minimum = new decimal(new int[] { 5, 0, 0, int.MinValue });
            Translate1.Name = "Translate1";
            Translate1.Size = new Size(56, 26);
            Translate1.TabIndex = 11;
            // 
            // Translate2
            // 
            Translate2.DecimalPlaces = 2;
            Translate2.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            Translate2.Location = new System.Drawing.Point(1038, 225);
            Translate2.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            Translate2.Minimum = new decimal(new int[] { 5, 0, 0, int.MinValue });
            Translate2.Name = "Translate2";
            Translate2.Size = new Size(56, 26);
            Translate2.TabIndex = 12;
            // 
            // Translate3
            // 
            Translate3.DecimalPlaces = 2;
            Translate3.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            Translate3.Location = new System.Drawing.Point(1106, 225);
            Translate3.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            Translate3.Minimum = new decimal(new int[] { 5, 0, 0, int.MinValue });
            Translate3.Name = "Translate3";
            Translate3.Size = new Size(54, 26);
            Translate3.TabIndex = 13;
            // 
            // XLable
            // 
            XLable.AutoSize = true;
            XLable.Location = new System.Drawing.Point(987, 203);
            XLable.Name = "XLable";
            XLable.Size = new Size(20, 19);
            XLable.TabIndex = 14;
            XLable.Text = "X";
            // 
            // YLable
            // 
            YLable.AutoSize = true;
            YLable.Location = new System.Drawing.Point(1055, 203);
            YLable.Name = "YLable";
            YLable.Size = new Size(20, 19);
            YLable.TabIndex = 15;
            YLable.Text = "Y";
            // 
            // ZLable
            // 
            ZLable.AutoSize = true;
            ZLable.Location = new System.Drawing.Point(1122, 203);
            ZLable.Name = "ZLable";
            ZLable.Size = new Size(18, 19);
            ZLable.TabIndex = 16;
            ZLable.Text = "Z";
            // 
            // OffsetLable
            // 
            OffsetLable.AutoSize = true;
            OffsetLable.Location = new System.Drawing.Point(1166, 232);
            OffsetLable.Name = "OffsetLable";
            OffsetLable.Size = new Size(46, 19);
            OffsetLable.TabIndex = 17;
            OffsetLable.Text = "Offset";
            // 
            // Rotate1
            // 
            Rotate1.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            Rotate1.Location = new System.Drawing.Point(970, 257);
            Rotate1.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            Rotate1.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            Rotate1.Name = "Rotate1";
            Rotate1.Size = new Size(54, 26);
            Rotate1.TabIndex = 20;
            // 
            // Rotate2
            // 
            Rotate2.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            Rotate2.Location = new System.Drawing.Point(1038, 257);
            Rotate2.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            Rotate2.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            Rotate2.Name = "Rotate2";
            Rotate2.Size = new Size(56, 26);
            Rotate2.TabIndex = 19;
            // 
            // Rotate3
            // 
            Rotate3.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            Rotate3.Location = new System.Drawing.Point(1106, 257);
            Rotate3.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            Rotate3.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            Rotate3.Name = "Rotate3";
            Rotate3.Size = new Size(54, 26);
            Rotate3.TabIndex = 18;
            // 
            // Scale1
            // 
            Scale1.DecimalPlaces = 1;
            Scale1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            Scale1.Location = new System.Drawing.Point(970, 289);
            Scale1.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            Scale1.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            Scale1.Name = "Scale1";
            Scale1.Size = new Size(54, 26);
            Scale1.TabIndex = 23;
            Scale1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Scale2
            // 
            Scale2.DecimalPlaces = 1;
            Scale2.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            Scale2.Location = new System.Drawing.Point(1038, 289);
            Scale2.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            Scale2.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            Scale2.Name = "Scale2";
            Scale2.Size = new Size(56, 26);
            Scale2.TabIndex = 22;
            Scale2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Scale3
            // 
            Scale3.DecimalPlaces = 1;
            Scale3.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            Scale3.Location = new System.Drawing.Point(1106, 289);
            Scale3.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            Scale3.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            Scale3.Name = "Scale3";
            Scale3.Size = new Size(54, 26);
            Scale3.TabIndex = 21;
            Scale3.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // RotateLable
            // 
            RotateLable.AutoSize = true;
            RotateLable.Location = new System.Drawing.Point(1166, 264);
            RotateLable.Name = "RotateLable";
            RotateLable.Size = new Size(49, 19);
            RotateLable.TabIndex = 24;
            RotateLable.Text = "Rotate";
            // 
            // ScaleLable
            // 
            ScaleLable.AutoSize = true;
            ScaleLable.Location = new System.Drawing.Point(1166, 296);
            ScaleLable.Name = "ScaleLable";
            ScaleLable.Size = new Size(42, 19);
            ScaleLable.TabIndex = 25;
            ScaleLable.Text = "Scale";
            // 
            // ApplyAffin
            // 
            ApplyAffin.Location = new System.Drawing.Point(970, 321);
            ApplyAffin.Name = "ApplyAffin";
            ApplyAffin.Size = new Size(190, 34);
            ApplyAffin.TabIndex = 26;
            ApplyAffin.Text = "Apply";
            ApplyAffin.UseVisualStyleBackColor = true;
            ApplyAffin.Click += ApplyAffin_Click;
            // 
            // ReflectionComboBox
            // 
            ReflectionComboBox.FormattingEnabled = true;
            ReflectionComboBox.Items.AddRange(new object[] { "X", "Y", "Z" });
            ReflectionComboBox.Location = new System.Drawing.Point(970, 380);
            ReflectionComboBox.Name = "ReflectionComboBox";
            ReflectionComboBox.Size = new Size(190, 27);
            ReflectionComboBox.TabIndex = 27;
            // 
            // ApplyReflection
            // 
            ApplyReflection.Location = new System.Drawing.Point(970, 413);
            ApplyReflection.Name = "ApplyReflection";
            ApplyReflection.Size = new Size(190, 34);
            ApplyReflection.TabIndex = 28;
            ApplyReflection.Text = "Apply";
            ApplyReflection.UseVisualStyleBackColor = true;
            ApplyReflection.Click += ApplyReflection_Click;
            // 
            // Reflectionlable
            // 
            Reflectionlable.AutoSize = true;
            Reflectionlable.Location = new System.Drawing.Point(966, 358);
            Reflectionlable.Name = "Reflectionlable";
            Reflectionlable.Size = new Size(69, 19);
            Reflectionlable.TabIndex = 29;
            Reflectionlable.Text = "Reflection";
            // 
            // GeneratrixX
            // 
            GeneratrixX.DecimalPlaces = 1;
            GeneratrixX.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            GeneratrixX.Location = new System.Drawing.Point(1225, 343);
            GeneratrixX.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            GeneratrixX.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            GeneratrixX.Name = "GeneratrixX";
            GeneratrixX.Size = new Size(54, 26);
            GeneratrixX.TabIndex = 43;
            // 
            // GeneratrixY
            // 
            GeneratrixY.DecimalPlaces = 1;
            GeneratrixY.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            GeneratrixY.Location = new System.Drawing.Point(1292, 343);
            GeneratrixY.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            GeneratrixY.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            GeneratrixY.Name = "GeneratrixY";
            GeneratrixY.Size = new Size(56, 26);
            GeneratrixY.TabIndex = 42;
            // 
            // GeneratrixZ
            // 
            GeneratrixZ.DecimalPlaces = 1;
            GeneratrixZ.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            GeneratrixZ.Location = new System.Drawing.Point(1359, 343);
            GeneratrixZ.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            GeneratrixZ.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            GeneratrixZ.Name = "GeneratrixZ";
            GeneratrixZ.Size = new Size(56, 26);
            GeneratrixZ.TabIndex = 41;
            // 
            // Point2X
            // 
            Point2X.DecimalPlaces = 1;
            Point2X.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            Point2X.Location = new System.Drawing.Point(970, 513);
            Point2X.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            Point2X.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            Point2X.Name = "Point2X";
            Point2X.Size = new Size(54, 26);
            Point2X.TabIndex = 46;
            // 
            // Point2Y
            // 
            Point2Y.DecimalPlaces = 1;
            Point2Y.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            Point2Y.Location = new System.Drawing.Point(1036, 513);
            Point2Y.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            Point2Y.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            Point2Y.Name = "Point2Y";
            Point2Y.Size = new Size(56, 26);
            Point2Y.TabIndex = 45;
            // 
            // Point2Z
            // 
            Point2Z.DecimalPlaces = 1;
            Point2Z.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            Point2Z.Location = new System.Drawing.Point(1104, 513);
            Point2Z.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            Point2Z.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            Point2Z.Name = "Point2Z";
            Point2Z.Size = new Size(56, 26);
            Point2Z.TabIndex = 44;
            // 
            // RotateAroundLineLable
            // 
            RotateAroundLineLable.AutoSize = true;
            RotateAroundLineLable.Location = new System.Drawing.Point(966, 459);
            RotateAroundLineLable.Name = "RotateAroundLineLable";
            RotateAroundLineLable.Size = new Size(119, 19);
            RotateAroundLineLable.TabIndex = 47;
            RotateAroundLineLable.Text = "Rotate around line";
            // 
            // Angle
            // 
            Angle.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            Angle.Location = new System.Drawing.Point(970, 545);
            Angle.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            Angle.Minimum = new decimal(new int[] { 180, 0, 0, int.MinValue });
            Angle.Name = "Angle";
            Angle.Size = new Size(190, 26);
            Angle.TabIndex = 48;
            // 
            // ApplyLineRotation
            // 
            ApplyLineRotation.Location = new System.Drawing.Point(970, 577);
            ApplyLineRotation.Name = "ApplyLineRotation";
            ApplyLineRotation.Size = new Size(190, 34);
            ApplyLineRotation.TabIndex = 49;
            ApplyLineRotation.Text = "Apply";
            ApplyLineRotation.UseVisualStyleBackColor = true;
            ApplyLineRotation.Click += ApplyLineRotation_Click;
            // 
            // AngleLabel
            // 
            AngleLabel.AutoSize = true;
            AngleLabel.Location = new System.Drawing.Point(1166, 552);
            AngleLabel.Name = "AngleLabel";
            AngleLabel.Size = new Size(44, 19);
            AngleLabel.TabIndex = 47;
            AngleLabel.Text = "Angle";
            // 
            // Point1Lable
            // 
            Point1Lable.AutoSize = true;
            Point1Lable.Location = new System.Drawing.Point(1166, 488);
            Point1Lable.Name = "Point1Lable";
            Point1Lable.Size = new Size(52, 19);
            Point1Lable.TabIndex = 47;
            Point1Lable.Text = "Point 1";
            // 
            // Point2Lable
            // 
            Point2Lable.AutoSize = true;
            Point2Lable.Location = new System.Drawing.Point(1166, 520);
            Point2Lable.Name = "Point2Lable";
            Point2Lable.Size = new Size(52, 19);
            Point2Lable.TabIndex = 47;
            Point2Lable.Text = "Point 2";
            // 
            // SaveButton
            // 
            SaveButton.Location = new System.Drawing.Point(1225, 34);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(190, 34);
            SaveButton.TabIndex = 50;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // UploadButton
            // 
            UploadButton.Location = new System.Drawing.Point(1225, 74);
            UploadButton.Name = "UploadButton";
            UploadButton.Size = new Size(190, 34);
            UploadButton.TabIndex = 51;
            UploadButton.Text = "Upload";
            UploadButton.UseVisualStyleBackColor = true;
            UploadButton.Click += UploadButton_Click;
            // 
            // GeneratrixListBox
            // 
            GeneratrixListBox.FormattingEnabled = true;
            GeneratrixListBox.ItemHeight = 19;
            GeneratrixListBox.Location = new System.Drawing.Point(1225, 142);
            GeneratrixListBox.Name = "GeneratrixListBox";
            GeneratrixListBox.Size = new Size(190, 194);
            GeneratrixListBox.TabIndex = 52;
            // 
            // GeneratrixLabel
            // 
            GeneratrixLabel.AutoSize = true;
            GeneratrixLabel.Location = new System.Drawing.Point(1225, 120);
            GeneratrixLabel.Name = "GeneratrixLabel";
            GeneratrixLabel.Size = new Size(72, 19);
            GeneratrixLabel.TabIndex = 53;
            GeneratrixLabel.Text = "Generatrix";
            // 
            // AddPointGeneratrix
            // 
            AddPointGeneratrix.Location = new System.Drawing.Point(1225, 375);
            AddPointGeneratrix.Name = "AddPointGeneratrix";
            AddPointGeneratrix.Size = new Size(84, 34);
            AddPointGeneratrix.TabIndex = 54;
            AddPointGeneratrix.Text = "Add";
            AddPointGeneratrix.UseVisualStyleBackColor = true;
            AddPointGeneratrix.Click += AddPointGeneratrix_Click;
            // 
            // DeletePointGeneratrix
            // 
            DeletePointGeneratrix.Location = new System.Drawing.Point(1315, 375);
            DeletePointGeneratrix.Name = "DeletePointGeneratrix";
            DeletePointGeneratrix.Size = new Size(100, 34);
            DeletePointGeneratrix.TabIndex = 55;
            DeletePointGeneratrix.Text = "Delete";
            DeletePointGeneratrix.UseVisualStyleBackColor = true;
            DeletePointGeneratrix.Click += DeletePointGeneratrix_Click;
            // 
            // PartitionLabel
            // 
            PartitionLabel.AutoSize = true;
            PartitionLabel.Location = new System.Drawing.Point(1225, 421);
            PartitionLabel.Name = "PartitionLabel";
            PartitionLabel.Size = new Size(134, 19);
            PartitionLabel.TabIndex = 56;
            PartitionLabel.Text = "Number of partitions";
            // 
            // Partition
            // 
            Partition.Location = new System.Drawing.Point(1225, 443);
            Partition.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            Partition.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            Partition.Name = "Partition";
            Partition.Size = new Size(190, 26);
            Partition.TabIndex = 57;
            Partition.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Build
            // 
            Build.Location = new System.Drawing.Point(1225, 533);
            Build.Name = "Build";
            Build.Size = new Size(190, 34);
            Build.TabIndex = 58;
            Build.Text = "Build";
            Build.UseVisualStyleBackColor = true;
            Build.Click += Build_Click;
            // 
            // Point1X
            // 
            Point1X.DecimalPlaces = 1;
            Point1X.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            Point1X.Location = new System.Drawing.Point(970, 481);
            Point1X.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            Point1X.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            Point1X.Name = "Point1X";
            Point1X.Size = new Size(54, 26);
            Point1X.TabIndex = 61;
            // 
            // Point1Y
            // 
            Point1Y.DecimalPlaces = 1;
            Point1Y.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            Point1Y.Location = new System.Drawing.Point(1037, 481);
            Point1Y.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            Point1Y.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            Point1Y.Name = "Point1Y";
            Point1Y.Size = new Size(56, 26);
            Point1Y.TabIndex = 60;
            // 
            // Point1Z
            // 
            Point1Z.DecimalPlaces = 1;
            Point1Z.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            Point1Z.Location = new System.Drawing.Point(1104, 481);
            Point1Z.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            Point1Z.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            Point1Z.Name = "Point1Z";
            Point1Z.Size = new Size(56, 26);
            Point1Z.TabIndex = 59;
            // 
            // AxisLabel
            // 
            AxisLabel.AutoSize = true;
            AxisLabel.Location = new System.Drawing.Point(1225, 478);
            AxisLabel.Name = "AxisLabel";
            AxisLabel.Size = new Size(36, 19);
            AxisLabel.TabIndex = 64;
            AxisLabel.Text = "Axis";
            // 
            // AxisComboBox
            // 
            AxisComboBox.FormattingEnabled = true;
            AxisComboBox.Items.AddRange(new object[] { "OX", "OY", "OZ" });
            AxisComboBox.Location = new System.Drawing.Point(1225, 500);
            AxisComboBox.Name = "AxisComboBox";
            AxisComboBox.Size = new Size(190, 27);
            AxisComboBox.TabIndex = 62;
            // 
            // FunctionTextBox
            // 
            FunctionTextBox.Location = new System.Drawing.Point(12, 636);
            FunctionTextBox.Name = "FunctionTextBox";
            FunctionTextBox.Size = new Size(370, 26);
            FunctionTextBox.TabIndex = 65;
            // 
            // FunctionLabel
            // 
            FunctionLabel.AutoSize = true;
            FunctionLabel.Location = new System.Drawing.Point(12, 614);
            FunctionLabel.Name = "FunctionLabel";
            FunctionLabel.Size = new Size(61, 19);
            FunctionLabel.TabIndex = 66;
            FunctionLabel.Text = "Function";
            // 
            // FunctionX0UpDown
            // 
            FunctionX0UpDown.DecimalPlaces = 1;
            FunctionX0UpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            FunctionX0UpDown.Location = new System.Drawing.Point(12, 685);
            FunctionX0UpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            FunctionX0UpDown.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            FunctionX0UpDown.Name = "FunctionX0UpDown";
            FunctionX0UpDown.Size = new Size(54, 26);
            FunctionX0UpDown.TabIndex = 67;
            // 
            // FunctionX1UpDown
            // 
            FunctionX1UpDown.DecimalPlaces = 1;
            FunctionX1UpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            FunctionX1UpDown.Location = new System.Drawing.Point(72, 685);
            FunctionX1UpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            FunctionX1UpDown.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            FunctionX1UpDown.Name = "FunctionX1UpDown";
            FunctionX1UpDown.Size = new Size(54, 26);
            FunctionX1UpDown.TabIndex = 68;
            // 
            // FunctionY0UpDown
            // 
            FunctionY0UpDown.DecimalPlaces = 1;
            FunctionY0UpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            FunctionY0UpDown.Location = new System.Drawing.Point(12, 738);
            FunctionY0UpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            FunctionY0UpDown.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            FunctionY0UpDown.Name = "FunctionY0UpDown";
            FunctionY0UpDown.Size = new Size(54, 26);
            FunctionY0UpDown.TabIndex = 69;
            // 
            // FunctionY1UpDown
            // 
            FunctionY1UpDown.DecimalPlaces = 1;
            FunctionY1UpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            FunctionY1UpDown.Location = new System.Drawing.Point(72, 738);
            FunctionY1UpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            FunctionY1UpDown.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            FunctionY1UpDown.Name = "FunctionY1UpDown";
            FunctionY1UpDown.Size = new Size(54, 26);
            FunctionY1UpDown.TabIndex = 70;
            // 
            // FunctionX0Label
            // 
            FunctionX0Label.AutoSize = true;
            FunctionX0Label.Location = new System.Drawing.Point(12, 663);
            FunctionX0Label.Name = "FunctionX0Label";
            FunctionX0Label.Size = new Size(28, 19);
            FunctionX0Label.TabIndex = 71;
            FunctionX0Label.Text = "X0";
            // 
            // FunctionX1Label
            // 
            FunctionX1Label.AutoSize = true;
            FunctionX1Label.Location = new System.Drawing.Point(72, 663);
            FunctionX1Label.Name = "FunctionX1Label";
            FunctionX1Label.Size = new Size(28, 19);
            FunctionX1Label.TabIndex = 72;
            FunctionX1Label.Text = "X1";
            // 
            // FunctionY0Label
            // 
            FunctionY0Label.AutoSize = true;
            FunctionY0Label.Location = new System.Drawing.Point(12, 716);
            FunctionY0Label.Name = "FunctionY0Label";
            FunctionY0Label.Size = new Size(28, 19);
            FunctionY0Label.TabIndex = 73;
            FunctionY0Label.Text = "Y0";
            // 
            // FunctionY1Label
            // 
            FunctionY1Label.AutoSize = true;
            FunctionY1Label.Location = new System.Drawing.Point(72, 716);
            FunctionY1Label.Name = "FunctionY1Label";
            FunctionY1Label.Size = new Size(28, 19);
            FunctionY1Label.TabIndex = 74;
            FunctionY1Label.Text = "Y1";
            // 
            // FunctionXStepUpDown
            // 
            FunctionXStepUpDown.DecimalPlaces = 2;
            FunctionXStepUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            FunctionXStepUpDown.Location = new System.Drawing.Point(132, 685);
            FunctionXStepUpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            FunctionXStepUpDown.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            FunctionXStepUpDown.Name = "FunctionXStepUpDown";
            FunctionXStepUpDown.Size = new Size(54, 26);
            FunctionXStepUpDown.TabIndex = 75;
            // 
            // FunctionYStepUpDown
            // 
            FunctionYStepUpDown.DecimalPlaces = 2;
            FunctionYStepUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            FunctionYStepUpDown.Location = new System.Drawing.Point(132, 738);
            FunctionYStepUpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            FunctionYStepUpDown.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            FunctionYStepUpDown.Name = "FunctionYStepUpDown";
            FunctionYStepUpDown.Size = new Size(54, 26);
            FunctionYStepUpDown.TabIndex = 76;
            // 
            // FunctionXStepLabel
            // 
            FunctionXStepLabel.AutoSize = true;
            FunctionXStepLabel.Location = new System.Drawing.Point(132, 663);
            FunctionXStepLabel.Name = "FunctionXStepLabel";
            FunctionXStepLabel.Size = new Size(52, 19);
            FunctionXStepLabel.TabIndex = 77;
            FunctionXStepLabel.Text = "X Step";
            // 
            // FunctionYStepLabel
            // 
            FunctionYStepLabel.AutoSize = true;
            FunctionYStepLabel.Location = new System.Drawing.Point(132, 716);
            FunctionYStepLabel.Name = "FunctionYStepLabel";
            FunctionYStepLabel.Size = new Size(51, 19);
            FunctionYStepLabel.TabIndex = 78;
            FunctionYStepLabel.Text = "Y Step";
            // 
            // FunctionBuild
            // 
            FunctionBuild.Location = new System.Drawing.Point(12, 770);
            FunctionBuild.Name = "FunctionBuild";
            FunctionBuild.Size = new Size(174, 34);
            FunctionBuild.TabIndex = 79;
            FunctionBuild.Text = "Build";
            FunctionBuild.UseVisualStyleBackColor = true;
            FunctionBuild.Click += FunctionBuild_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1428, 809);
            Controls.Add(FunctionBuild);
            Controls.Add(FunctionYStepLabel);
            Controls.Add(FunctionXStepLabel);
            Controls.Add(FunctionYStepUpDown);
            Controls.Add(FunctionXStepUpDown);
            Controls.Add(FunctionY1Label);
            Controls.Add(FunctionY0Label);
            Controls.Add(FunctionX1Label);
            Controls.Add(FunctionX0Label);
            Controls.Add(FunctionY1UpDown);
            Controls.Add(FunctionY0UpDown);
            Controls.Add(FunctionX1UpDown);
            Controls.Add(FunctionX0UpDown);
            Controls.Add(FunctionLabel);
            Controls.Add(FunctionTextBox);
            Controls.Add(AxisLabel);
            Controls.Add(AxisComboBox);
            Controls.Add(Point1X);
            Controls.Add(Point1Y);
            Controls.Add(Point1Z);
            Controls.Add(Build);
            Controls.Add(Partition);
            Controls.Add(PartitionLabel);
            Controls.Add(DeletePointGeneratrix);
            Controls.Add(AddPointGeneratrix);
            Controls.Add(GeneratrixLabel);
            Controls.Add(GeneratrixListBox);
            Controls.Add(UploadButton);
            Controls.Add(SaveButton);
            Controls.Add(ApplyLineRotation);
            Controls.Add(Angle);
            Controls.Add(Point2Lable);
            Controls.Add(Point1Lable);
            Controls.Add(AngleLabel);
            Controls.Add(RotateAroundLineLable);
            Controls.Add(Point2X);
            Controls.Add(Point2Y);
            Controls.Add(Point2Z);
            Controls.Add(GeneratrixX);
            Controls.Add(GeneratrixY);
            Controls.Add(GeneratrixZ);
            Controls.Add(Reflectionlable);
            Controls.Add(ApplyReflection);
            Controls.Add(ReflectionComboBox);
            Controls.Add(ApplyAffin);
            Controls.Add(ScaleLable);
            Controls.Add(RotateLable);
            Controls.Add(Scale1);
            Controls.Add(Scale2);
            Controls.Add(Scale3);
            Controls.Add(Rotate1);
            Controls.Add(Rotate2);
            Controls.Add(Rotate3);
            Controls.Add(OffsetLable);
            Controls.Add(ZLable);
            Controls.Add(YLable);
            Controls.Add(XLable);
            Controls.Add(Translate3);
            Controls.Add(Translate2);
            Controls.Add(Translate1);
            Controls.Add(ApplyPrimitive);
            Controls.Add(PrimitiveLabel);
            Controls.Add(PerspectiveLabel);
            Controls.Add(PrimitiveComboBox);
            Controls.Add(ApplyPerspective);
            Controls.Add(PerspectiveComboBox);
            Controls.Add(Box);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Box).EndInit();
            ((System.ComponentModel.ISupportInitialize)Translate1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Translate2).EndInit();
            ((System.ComponentModel.ISupportInitialize)Translate3).EndInit();
            ((System.ComponentModel.ISupportInitialize)Rotate1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Rotate2).EndInit();
            ((System.ComponentModel.ISupportInitialize)Rotate3).EndInit();
            ((System.ComponentModel.ISupportInitialize)Scale1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Scale2).EndInit();
            ((System.ComponentModel.ISupportInitialize)Scale3).EndInit();
            ((System.ComponentModel.ISupportInitialize)GeneratrixX).EndInit();
            ((System.ComponentModel.ISupportInitialize)GeneratrixY).EndInit();
            ((System.ComponentModel.ISupportInitialize)GeneratrixZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)Point2X).EndInit();
            ((System.ComponentModel.ISupportInitialize)Point2Y).EndInit();
            ((System.ComponentModel.ISupportInitialize)Point2Z).EndInit();
            ((System.ComponentModel.ISupportInitialize)Angle).EndInit();
            ((System.ComponentModel.ISupportInitialize)Partition).EndInit();
            ((System.ComponentModel.ISupportInitialize)Point1X).EndInit();
            ((System.ComponentModel.ISupportInitialize)Point1Y).EndInit();
            ((System.ComponentModel.ISupportInitialize)Point1Z).EndInit();
            ((System.ComponentModel.ISupportInitialize)FunctionX0UpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)FunctionX1UpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)FunctionY0UpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)FunctionY1UpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)FunctionXStepUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)FunctionYStepUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox Box;
        private ComboBox PerspectiveComboBox;
        private Button ApplyPerspective;
        private ComboBox PrimitiveComboBox;
        private Label PerspectiveLabel;
        private Label PrimitiveLabel;
        private Button ApplyPrimitive;
        private NumericUpDown Translate1;
        private NumericUpDown Translate2;
        private NumericUpDown Translate3;
        private Label XLable;
        private Label YLable;
        private Label ZLable;
        private Label OffsetLable;
        private NumericUpDown Rotate1;
        private NumericUpDown Rotate2;
        private NumericUpDown Rotate3;
        private NumericUpDown Scale1;
        private NumericUpDown Scale2;
        private NumericUpDown Scale3;
        private Label RotateLable;
        private Label ScaleLable;
        private Button ApplyAffin;
        private ComboBox ReflectionComboBox;
        private Button ApplyReflection;
        private Label Reflectionlable;
        private NumericUpDown GeneratrixX;
        private NumericUpDown GeneratrixY;
        private NumericUpDown GeneratrixZ;
        private NumericUpDown Point2X;
        private NumericUpDown Point2Y;
        private NumericUpDown Point2Z;
        private Label RotateAroundLineLable;
        private NumericUpDown Angle;
        private Button ApplyLineRotation;
        private Label AngleLabel;
        private Label Point1Lable;
        private Label Point2Lable;
        private Button SaveButton;
        private Button UploadButton;
        private ListBox GeneratrixListBox;
        private Label GeneratrixLabel;
        private Button AddPointGeneratrix;
        private Button DeletePointGeneratrix;
        private Label PartitionLabel;
        private NumericUpDown Partition;
        private Button Build;
        private NumericUpDown Point1X;
        private NumericUpDown Point1Y;
        private NumericUpDown Point1Z;
        private Label AxisLabel;
        private ComboBox AxisComboBox;
        private TextBox FunctionTextBox;
        private Label FunctionLabel;
        private NumericUpDown FunctionX0UpDown;
        private NumericUpDown FunctionX1UpDown;
        private NumericUpDown FunctionY0UpDown;
        private NumericUpDown FunctionY1UpDown;
        private Label FunctionX0Label;
        private Label FunctionX1Label;
        private Label FunctionY0Label;
        private Label FunctionY1Label;
        private NumericUpDown FunctionXStepUpDown;
        private NumericUpDown FunctionYStepUpDown;
        private Label FunctionXStepLabel;
        private Label FunctionYStepLabel;
        private Button FunctionBuild;
    }
}

