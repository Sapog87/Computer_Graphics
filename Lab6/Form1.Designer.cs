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
            this.Box = new System.Windows.Forms.PictureBox();
            this.PerspectiveComboBox = new System.Windows.Forms.ComboBox();
            this.ApplyPerspective = new System.Windows.Forms.Button();
            this.PrimitiveComboBox = new System.Windows.Forms.ComboBox();
            this.PerspectiveLabel = new System.Windows.Forms.Label();
            this.PrimitiveLabel = new System.Windows.Forms.Label();
            this.ApplyPrimitive = new System.Windows.Forms.Button();
            this.Translate1 = new System.Windows.Forms.NumericUpDown();
            this.Translate2 = new System.Windows.Forms.NumericUpDown();
            this.Translate3 = new System.Windows.Forms.NumericUpDown();
            this.XLable = new System.Windows.Forms.Label();
            this.YLable = new System.Windows.Forms.Label();
            this.ZLable = new System.Windows.Forms.Label();
            this.OffsetLable = new System.Windows.Forms.Label();
            this.Rotate1 = new System.Windows.Forms.NumericUpDown();
            this.Rotate2 = new System.Windows.Forms.NumericUpDown();
            this.Rotate3 = new System.Windows.Forms.NumericUpDown();
            this.Scale1 = new System.Windows.Forms.NumericUpDown();
            this.Scale2 = new System.Windows.Forms.NumericUpDown();
            this.Scale3 = new System.Windows.Forms.NumericUpDown();
            this.RotateLable = new System.Windows.Forms.Label();
            this.ScaleLable = new System.Windows.Forms.Label();
            this.ApplyAffin = new System.Windows.Forms.Button();
            this.ReflectionComboBox = new System.Windows.Forms.ComboBox();
            this.ApplyReflection = new System.Windows.Forms.Button();
            this.Reflectionlable = new System.Windows.Forms.Label();
            this.Point1X = new System.Windows.Forms.NumericUpDown();
            this.Point1Y = new System.Windows.Forms.NumericUpDown();
            this.Point1Z = new System.Windows.Forms.NumericUpDown();
            this.Point2X = new System.Windows.Forms.NumericUpDown();
            this.Point2Y = new System.Windows.Forms.NumericUpDown();
            this.Point2Z = new System.Windows.Forms.NumericUpDown();
            this.RotateAroundLineLable = new System.Windows.Forms.Label();
            this.Angle = new System.Windows.Forms.NumericUpDown();
            this.ApplyLineRotation = new System.Windows.Forms.Button();
            this.AngleLabel = new System.Windows.Forms.Label();
            this.Point1Lable = new System.Windows.Forms.Label();
            this.Point2Lable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Translate1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Translate2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Translate3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotate1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotate2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotate3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scale1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scale2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scale3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point1X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point1Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point1Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point2X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point2Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point2Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Angle)).BeginInit();
            this.SuspendLayout();
            // 
            // Box
            // 
            this.Box.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Box.Location = new System.Drawing.Point(12, 12);
            this.Box.Name = "Box";
            this.Box.Size = new System.Drawing.Size(948, 657);
            this.Box.TabIndex = 0;
            this.Box.TabStop = false;
            // 
            // PerspectiveComboBox
            // 
            this.PerspectiveComboBox.FormattingEnabled = true;
            this.PerspectiveComboBox.Items.AddRange(new object[] {
            "Perspective",
            "Isometric",
            "Orthographic XY",
            "Orthographic XZ",
            "Orthographic YZ"});
            this.PerspectiveComboBox.Location = new System.Drawing.Point(970, 34);
            this.PerspectiveComboBox.Name = "PerspectiveComboBox";
            this.PerspectiveComboBox.Size = new System.Drawing.Size(190, 27);
            this.PerspectiveComboBox.TabIndex = 2;
            // 
            // ApplyPerspective
            // 
            this.ApplyPerspective.Location = new System.Drawing.Point(970, 67);
            this.ApplyPerspective.Name = "ApplyPerspective";
            this.ApplyPerspective.Size = new System.Drawing.Size(190, 34);
            this.ApplyPerspective.TabIndex = 4;
            this.ApplyPerspective.Text = "Apply";
            this.ApplyPerspective.UseVisualStyleBackColor = true;
            this.ApplyPerspective.Click += new System.EventHandler(this.ApplyPerspective_Click);
            // 
            // PrimitiveComboBox
            // 
            this.PrimitiveComboBox.FormattingEnabled = true;
            this.PrimitiveComboBox.Items.AddRange(new object[] {
            "Tetrahedron",
            "Hexahedron",
            "Octahedron"});
            this.PrimitiveComboBox.Location = new System.Drawing.Point(970, 130);
            this.PrimitiveComboBox.Name = "PrimitiveComboBox";
            this.PrimitiveComboBox.Size = new System.Drawing.Size(190, 27);
            this.PrimitiveComboBox.TabIndex = 6;
            // 
            // PerspectiveLabel
            // 
            this.PerspectiveLabel.AutoSize = true;
            this.PerspectiveLabel.Location = new System.Drawing.Point(966, 12);
            this.PerspectiveLabel.Name = "PerspectiveLabel";
            this.PerspectiveLabel.Size = new System.Drawing.Size(121, 19);
            this.PerspectiveLabel.TabIndex = 7;
            this.PerspectiveLabel.Text = "Choose projection";
            // 
            // PrimitiveLabel
            // 
            this.PrimitiveLabel.AutoSize = true;
            this.PrimitiveLabel.Location = new System.Drawing.Point(966, 106);
            this.PrimitiveLabel.Name = "PrimitiveLabel";
            this.PrimitiveLabel.Size = new System.Drawing.Size(78, 19);
            this.PrimitiveLabel.TabIndex = 9;
            this.PrimitiveLabel.Text = "Polyhedron";
            // 
            // ApplyPrimitive
            // 
            this.ApplyPrimitive.Location = new System.Drawing.Point(970, 163);
            this.ApplyPrimitive.Name = "ApplyPrimitive";
            this.ApplyPrimitive.Size = new System.Drawing.Size(190, 34);
            this.ApplyPrimitive.TabIndex = 10;
            this.ApplyPrimitive.Text = "Apply";
            this.ApplyPrimitive.UseVisualStyleBackColor = true;
            this.ApplyPrimitive.Click += new System.EventHandler(this.ApplyPrimitive_Click);
            // 
            // Translate1
            // 
            this.Translate1.DecimalPlaces = 2;
            this.Translate1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.Translate1.Location = new System.Drawing.Point(970, 225);
            this.Translate1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Translate1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.Translate1.Name = "Translate1";
            this.Translate1.Size = new System.Drawing.Size(56, 26);
            this.Translate1.TabIndex = 11;
            // 
            // Translate2
            // 
            this.Translate2.DecimalPlaces = 2;
            this.Translate2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.Translate2.Location = new System.Drawing.Point(1038, 225);
            this.Translate2.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Translate2.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.Translate2.Name = "Translate2";
            this.Translate2.Size = new System.Drawing.Size(56, 26);
            this.Translate2.TabIndex = 12;
            // 
            // Translate3
            // 
            this.Translate3.DecimalPlaces = 2;
            this.Translate3.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.Translate3.Location = new System.Drawing.Point(1106, 225);
            this.Translate3.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Translate3.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.Translate3.Name = "Translate3";
            this.Translate3.Size = new System.Drawing.Size(54, 26);
            this.Translate3.TabIndex = 13;
            // 
            // XLable
            // 
            this.XLable.AutoSize = true;
            this.XLable.Location = new System.Drawing.Point(987, 203);
            this.XLable.Name = "label1";
            this.XLable.Size = new System.Drawing.Size(20, 19);
            this.XLable.TabIndex = 14;
            this.XLable.Text = "X";
            // 
            // YLable
            // 
            this.YLable.AutoSize = true;
            this.YLable.Location = new System.Drawing.Point(1055, 203);
            this.YLable.Name = "label2";
            this.YLable.Size = new System.Drawing.Size(20, 19);
            this.YLable.TabIndex = 15;
            this.YLable.Text = "Y";
            // 
            // ZLable
            // 
            this.ZLable.AutoSize = true;
            this.ZLable.Location = new System.Drawing.Point(1122, 203);
            this.ZLable.Name = "label3";
            this.ZLable.Size = new System.Drawing.Size(18, 19);
            this.ZLable.TabIndex = 16;
            this.ZLable.Text = "Z";
            // 
            // OffsetLable
            // 
            this.OffsetLable.AutoSize = true;
            this.OffsetLable.Location = new System.Drawing.Point(1166, 232);
            this.OffsetLable.Name = "label4";
            this.OffsetLable.Size = new System.Drawing.Size(46, 19);
            this.OffsetLable.TabIndex = 17;
            this.OffsetLable.Text = "Offset";
            // 
            // Rotate1
            // 
            this.Rotate1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Rotate1.Location = new System.Drawing.Point(970, 257);
            this.Rotate1.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.Rotate1.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.Rotate1.Name = "Rotate1";
            this.Rotate1.Size = new System.Drawing.Size(54, 26);
            this.Rotate1.TabIndex = 20;
            // 
            // Rotate2
            // 
            this.Rotate2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Rotate2.Location = new System.Drawing.Point(1038, 257);
            this.Rotate2.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.Rotate2.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.Rotate2.Name = "Rotate2";
            this.Rotate2.Size = new System.Drawing.Size(56, 26);
            this.Rotate2.TabIndex = 19;
            // 
            // Rotate3
            // 
            this.Rotate3.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Rotate3.Location = new System.Drawing.Point(1106, 257);
            this.Rotate3.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.Rotate3.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.Rotate3.Name = "Rotate3";
            this.Rotate3.Size = new System.Drawing.Size(54, 26);
            this.Rotate3.TabIndex = 18;
            // 
            // Scale1
            // 
            this.Scale1.DecimalPlaces = 1;
            this.Scale1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Scale1.Location = new System.Drawing.Point(970, 289);
            this.Scale1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Scale1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Scale1.Name = "Scale1";
            this.Scale1.Size = new System.Drawing.Size(54, 26);
            this.Scale1.TabIndex = 23;
            this.Scale1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Scale2
            // 
            this.Scale2.DecimalPlaces = 1;
            this.Scale2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Scale2.Location = new System.Drawing.Point(1038, 289);
            this.Scale2.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Scale2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Scale2.Name = "Scale2";
            this.Scale2.Size = new System.Drawing.Size(56, 26);
            this.Scale2.TabIndex = 22;
            this.Scale2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Scale3
            // 
            this.Scale3.DecimalPlaces = 1;
            this.Scale3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Scale3.Location = new System.Drawing.Point(1106, 289);
            this.Scale3.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Scale3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Scale3.Name = "Scale3";
            this.Scale3.Size = new System.Drawing.Size(54, 26);
            this.Scale3.TabIndex = 21;
            this.Scale3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // RotateLable
            // 
            this.RotateLable.AutoSize = true;
            this.RotateLable.Location = new System.Drawing.Point(1166, 264);
            this.RotateLable.Name = "label5";
            this.RotateLable.Size = new System.Drawing.Size(49, 19);
            this.RotateLable.TabIndex = 24;
            this.RotateLable.Text = "Rotate";
            // 
            // ScaleLable
            // 
            this.ScaleLable.AutoSize = true;
            this.ScaleLable.Location = new System.Drawing.Point(1166, 296);
            this.ScaleLable.Name = "label6";
            this.ScaleLable.Size = new System.Drawing.Size(42, 19);
            this.ScaleLable.TabIndex = 25;
            this.ScaleLable.Text = "Scale";
            // 
            // ApplyAffin
            // 
            this.ApplyAffin.Location = new System.Drawing.Point(970, 321);
            this.ApplyAffin.Name = "ApplyAffin";
            this.ApplyAffin.Size = new System.Drawing.Size(190, 34);
            this.ApplyAffin.TabIndex = 26;
            this.ApplyAffin.Text = "Apply";
            this.ApplyAffin.UseVisualStyleBackColor = true;
            this.ApplyAffin.Click += new System.EventHandler(this.ApplyAffin_Click);
            // 
            // ReflectionComboBox
            // 
            this.ReflectionComboBox.FormattingEnabled = true;
            this.ReflectionComboBox.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.ReflectionComboBox.Location = new System.Drawing.Point(970, 380);
            this.ReflectionComboBox.Name = "ReflectionComboBox";
            this.ReflectionComboBox.Size = new System.Drawing.Size(190, 27);
            this.ReflectionComboBox.TabIndex = 27;
            // 
            // ApplyReflection
            // 
            this.ApplyReflection.Location = new System.Drawing.Point(970, 413);
            this.ApplyReflection.Name = "ApplyReflection";
            this.ApplyReflection.Size = new System.Drawing.Size(190, 34);
            this.ApplyReflection.TabIndex = 28;
            this.ApplyReflection.Text = "Apply";
            this.ApplyReflection.UseVisualStyleBackColor = true;
            this.ApplyReflection.Click += new System.EventHandler(this.ApplyReflection_Click);
            // 
            // Reflectionlable
            // 
            this.Reflectionlable.AutoSize = true;
            this.Reflectionlable.Location = new System.Drawing.Point(966, 358);
            this.Reflectionlable.Name = "label7";
            this.Reflectionlable.Size = new System.Drawing.Size(69, 19);
            this.Reflectionlable.TabIndex = 29;
            this.Reflectionlable.Text = "Reflection";
            // 
            // Point1X
            // 
            this.Point1X.DecimalPlaces = 1;
            this.Point1X.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Point1X.Location = new System.Drawing.Point(970, 481);
            this.Point1X.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Point1X.Name = "Point1X";
            this.Point1X.Size = new System.Drawing.Size(54, 26);
            this.Point1X.TabIndex = 43;
            // 
            // Point1Y
            // 
            this.Point1Y.DecimalPlaces = 1;
            this.Point1Y.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Point1Y.Location = new System.Drawing.Point(1036, 481);
            this.Point1Y.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Point1Y.Name = "Point1Y";
            this.Point1Y.Size = new System.Drawing.Size(56, 26);
            this.Point1Y.TabIndex = 42;
            // 
            // Point1Z
            // 
            this.Point1Z.DecimalPlaces = 1;
            this.Point1Z.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Point1Z.Location = new System.Drawing.Point(1104, 481);
            this.Point1Z.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Point1Z.Name = "Point1Z";
            this.Point1Z.Size = new System.Drawing.Size(56, 26);
            this.Point1Z.TabIndex = 41;
            // 
            // Point2X
            // 
            this.Point2X.DecimalPlaces = 1;
            this.Point2X.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Point2X.Location = new System.Drawing.Point(970, 513);
            this.Point2X.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Point2X.Name = "Point2X";
            this.Point2X.Size = new System.Drawing.Size(54, 26);
            this.Point2X.TabIndex = 46;
            // 
            // Point2Y
            // 
            this.Point2Y.DecimalPlaces = 1;
            this.Point2Y.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Point2Y.Location = new System.Drawing.Point(1036, 513);
            this.Point2Y.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Point2Y.Name = "Point2Y";
            this.Point2Y.Size = new System.Drawing.Size(56, 26);
            this.Point2Y.TabIndex = 45;
            // 
            // Point2Z
            // 
            this.Point2Z.DecimalPlaces = 1;
            this.Point2Z.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Point2Z.Location = new System.Drawing.Point(1104, 513);
            this.Point2Z.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Point2Z.Name = "Point2Z";
            this.Point2Z.Size = new System.Drawing.Size(56, 26);
            this.Point2Z.TabIndex = 44;
            // 
            // RotateAroundLineLable
            // 
            this.RotateAroundLineLable.AutoSize = true;
            this.RotateAroundLineLable.Location = new System.Drawing.Point(966, 459);
            this.RotateAroundLineLable.Name = "label10";
            this.RotateAroundLineLable.Size = new System.Drawing.Size(119, 19);
            this.RotateAroundLineLable.TabIndex = 47;
            this.RotateAroundLineLable.Text = "Rotate around line";
            // 
            // Angle
            // 
            this.Angle.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Angle.Location = new System.Drawing.Point(970, 545);
            this.Angle.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.Angle.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.Angle.Name = "Angle";
            this.Angle.Size = new System.Drawing.Size(190, 26);
            this.Angle.TabIndex = 48;
            // 
            // ApplyLineRotation
            // 
            this.ApplyLineRotation.Location = new System.Drawing.Point(970, 577);
            this.ApplyLineRotation.Name = "ApplyLineRotation";
            this.ApplyLineRotation.Size = new System.Drawing.Size(190, 34);
            this.ApplyLineRotation.TabIndex = 49;
            this.ApplyLineRotation.Text = "Apply";
            this.ApplyLineRotation.UseVisualStyleBackColor = true;
            this.ApplyLineRotation.Click += new System.EventHandler(this.ApplyLineRotation_Click);
            // 
            // AngleLabel
            // 
            this.AngleLabel.AutoSize = true;
            this.AngleLabel.Location = new System.Drawing.Point(1166, 552);
            this.AngleLabel.Name = "label11";
            this.AngleLabel.Size = new System.Drawing.Size(44, 19);
            this.AngleLabel.TabIndex = 47;
            this.AngleLabel.Text = "Angle";
            // 
            // Point1Lable
            // 
            this.Point1Lable.AutoSize = true;
            this.Point1Lable.Location = new System.Drawing.Point(1166, 488);
            this.Point1Lable.Name = "label12";
            this.Point1Lable.Size = new System.Drawing.Size(48, 19);
            this.Point1Lable.TabIndex = 47;
            this.Point1Lable.Text = "Point 1";
            // 
            // Point2Lable
            // 
            this.Point2Lable.AutoSize = true;
            this.Point2Lable.Location = new System.Drawing.Point(1166, 520);
            this.Point2Lable.Name = "label13";
            this.Point2Lable.Size = new System.Drawing.Size(48, 19);
            this.Point2Lable.TabIndex = 47;
            this.Point2Lable.Text = "Point 2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 681);
            this.Controls.Add(this.ApplyLineRotation);
            this.Controls.Add(this.Angle);
            this.Controls.Add(this.Point2Lable);
            this.Controls.Add(this.Point1Lable);
            this.Controls.Add(this.AngleLabel);
            this.Controls.Add(this.RotateAroundLineLable);
            this.Controls.Add(this.Point2X);
            this.Controls.Add(this.Point2Y);
            this.Controls.Add(this.Point2Z);
            this.Controls.Add(this.Point1X);
            this.Controls.Add(this.Point1Y);
            this.Controls.Add(this.Point1Z);
            this.Controls.Add(this.Reflectionlable);
            this.Controls.Add(this.ApplyReflection);
            this.Controls.Add(this.ReflectionComboBox);
            this.Controls.Add(this.ApplyAffin);
            this.Controls.Add(this.ScaleLable);
            this.Controls.Add(this.RotateLable);
            this.Controls.Add(this.Scale1);
            this.Controls.Add(this.Scale2);
            this.Controls.Add(this.Scale3);
            this.Controls.Add(this.Rotate1);
            this.Controls.Add(this.Rotate2);
            this.Controls.Add(this.Rotate3);
            this.Controls.Add(this.OffsetLable);
            this.Controls.Add(this.ZLable);
            this.Controls.Add(this.YLable);
            this.Controls.Add(this.XLable);
            this.Controls.Add(this.Translate3);
            this.Controls.Add(this.Translate2);
            this.Controls.Add(this.Translate1);
            this.Controls.Add(this.ApplyPrimitive);
            this.Controls.Add(this.PrimitiveLabel);
            this.Controls.Add(this.PerspectiveLabel);
            this.Controls.Add(this.PrimitiveComboBox);
            this.Controls.Add(this.ApplyPerspective);
            this.Controls.Add(this.PerspectiveComboBox);
            this.Controls.Add(this.Box);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Translate1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Translate2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Translate3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotate1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotate2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rotate3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scale1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scale2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scale3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point1X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point1Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point1Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point2X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point2Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Point2Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Angle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Box;
        private System.Windows.Forms.ComboBox PerspectiveComboBox;
        private System.Windows.Forms.Button ApplyPerspective;
        private System.Windows.Forms.ComboBox PrimitiveComboBox;
        private System.Windows.Forms.Label PerspectiveLabel;
        private System.Windows.Forms.Label PrimitiveLabel;
        private System.Windows.Forms.Button ApplyPrimitive;
        private System.Windows.Forms.NumericUpDown Translate1;
        private System.Windows.Forms.NumericUpDown Translate2;
        private System.Windows.Forms.NumericUpDown Translate3;
        private System.Windows.Forms.Label XLable;
        private System.Windows.Forms.Label YLable;
        private System.Windows.Forms.Label ZLable;
        private System.Windows.Forms.Label OffsetLable;
        private System.Windows.Forms.NumericUpDown Rotate1;
        private System.Windows.Forms.NumericUpDown Rotate2;
        private System.Windows.Forms.NumericUpDown Rotate3;
        private System.Windows.Forms.NumericUpDown Scale1;
        private System.Windows.Forms.NumericUpDown Scale2;
        private System.Windows.Forms.NumericUpDown Scale3;
        private System.Windows.Forms.Label RotateLable;
        private System.Windows.Forms.Label ScaleLable;
        private System.Windows.Forms.Button ApplyAffin;
        private System.Windows.Forms.ComboBox ReflectionComboBox;
        private System.Windows.Forms.Button ApplyReflection;
        private System.Windows.Forms.Label Reflectionlable;
        private System.Windows.Forms.NumericUpDown Point1X;
        private System.Windows.Forms.NumericUpDown Point1Y;
        private System.Windows.Forms.NumericUpDown Point1Z;
        private System.Windows.Forms.NumericUpDown Point2X;
        private System.Windows.Forms.NumericUpDown Point2Y;
        private System.Windows.Forms.NumericUpDown Point2Z;
        private System.Windows.Forms.Label RotateAroundLineLable;
        private System.Windows.Forms.NumericUpDown Angle;
        private System.Windows.Forms.Button ApplyLineRotation;
        private System.Windows.Forms.Label AngleLabel;
        private System.Windows.Forms.Label Point1Lable;
        private System.Windows.Forms.Label Point2Lable;
    }
}

