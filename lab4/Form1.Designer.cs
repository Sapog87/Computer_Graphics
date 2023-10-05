namespace lab4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            primitivesLabel = new Label();
            pointRadioButton = new RadioButton();
            segmentRadioButton = new RadioButton();
            polygonRadioButton = new RadioButton();
            clearButton = new Button();
            movePolygonLabel = new Label();
            movePolygonNumericUpDownX = new NumericUpDown();
            movePolygonNumericUpDownY = new NumericUpDown();
            movePolygonButton = new Button();
            rotateSegmentLabel = new Label();
            rotateSegmentButton = new Button();
            rotatePolygonLabel = new Label();
            rotatePolygonCheckBox = new CheckBox();
            rotatePolygonNumericUpDownAngle = new NumericUpDown();
            rotatePolygonButton = new Button();
            scalePolygonButton = new Button();
            scalePolygonCheckBox = new CheckBox();
            scalePolygonLabel = new Label();
            scalePolygonNumericUpDownX = new NumericUpDown();
            scalePolygonNumericUpDownY = new NumericUpDown();
            findIntersectionsLabel = new Label();
            findIntersectionsButton = new Button();
            pictureBox = new PictureBox();
            intersectionLabel = new Label();
            pointInfo = new Label();
            label1 = new Label();
            belongsToConvex = new Label();
            belongsToNConvex = new Label();
            label2 = new Label();
            relativePointPosition = new Label();
            ((System.ComponentModel.ISupportInitialize)movePolygonNumericUpDownX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)movePolygonNumericUpDownY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rotatePolygonNumericUpDownAngle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scalePolygonNumericUpDownX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scalePolygonNumericUpDownY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // primitivesLabel
            // 
            primitivesLabel.AutoSize = true;
            primitivesLabel.Location = new Point(10, 9);
            primitivesLabel.Name = "primitivesLabel";
            primitivesLabel.Size = new Size(73, 15);
            primitivesLabel.TabIndex = 3;
            primitivesLabel.Text = "Примитивы";
            // 
            // pointRadioButton
            // 
            pointRadioButton.AutoSize = true;
            pointRadioButton.Location = new Point(10, 28);
            pointRadioButton.Name = "pointRadioButton";
            pointRadioButton.Size = new Size(56, 19);
            pointRadioButton.TabIndex = 0;
            pointRadioButton.TabStop = true;
            pointRadioButton.Text = "точка";
            pointRadioButton.UseVisualStyleBackColor = true;
            // 
            // segmentRadioButton
            // 
            segmentRadioButton.AutoSize = true;
            segmentRadioButton.Location = new Point(10, 52);
            segmentRadioButton.Name = "segmentRadioButton";
            segmentRadioButton.Size = new Size(68, 19);
            segmentRadioButton.TabIndex = 1;
            segmentRadioButton.TabStop = true;
            segmentRadioButton.Text = "отрезок";
            segmentRadioButton.UseVisualStyleBackColor = true;
            // 
            // polygonRadioButton
            // 
            polygonRadioButton.AutoSize = true;
            polygonRadioButton.Location = new Point(10, 77);
            polygonRadioButton.Name = "polygonRadioButton";
            polygonRadioButton.Size = new Size(72, 19);
            polygonRadioButton.TabIndex = 2;
            polygonRadioButton.TabStop = true;
            polygonRadioButton.Text = "полигон";
            polygonRadioButton.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            clearButton.Location = new Point(10, 102);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(75, 23);
            clearButton.TabIndex = 4;
            clearButton.Text = "Очистить";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // movePolygonLabel
            // 
            movePolygonLabel.AutoSize = true;
            movePolygonLabel.Location = new Point(100, 9);
            movePolygonLabel.Name = "movePolygonLabel";
            movePolygonLabel.Size = new Size(123, 15);
            movePolygonLabel.TabIndex = 5;
            movePolygonLabel.Text = "Смещение полигона";
            // 
            // movePolygonNumericUpDownX
            // 
            movePolygonNumericUpDownX.Location = new Point(100, 28);
            movePolygonNumericUpDownX.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            movePolygonNumericUpDownX.Name = "movePolygonNumericUpDownX";
            movePolygonNumericUpDownX.Size = new Size(75, 23);
            movePolygonNumericUpDownX.TabIndex = 23;
            // 
            // movePolygonNumericUpDownY
            // 
            movePolygonNumericUpDownY.Location = new Point(100, 57);
            movePolygonNumericUpDownY.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            movePolygonNumericUpDownY.Name = "movePolygonNumericUpDownY";
            movePolygonNumericUpDownY.Size = new Size(75, 23);
            movePolygonNumericUpDownY.TabIndex = 24;
            // 
            // movePolygonButton
            // 
            movePolygonButton.Location = new Point(100, 87);
            movePolygonButton.Name = "movePolygonButton";
            movePolygonButton.Size = new Size(75, 23);
            movePolygonButton.TabIndex = 8;
            movePolygonButton.Text = "Сместить";
            movePolygonButton.UseVisualStyleBackColor = true;
            movePolygonButton.Click += movePolygonButton_Click;
            // 
            // rotateSegmentLabel
            // 
            rotateSegmentLabel.AutoSize = true;
            rotateSegmentLabel.Location = new Point(10, 146);
            rotateSegmentLabel.Name = "rotateSegmentLabel";
            rotateSegmentLabel.Size = new Size(91, 15);
            rotateSegmentLabel.TabIndex = 9;
            rotateSegmentLabel.Text = "Поворот ребра";
            // 
            // rotateSegmentButton
            // 
            rotateSegmentButton.Location = new Point(10, 165);
            rotateSegmentButton.Name = "rotateSegmentButton";
            rotateSegmentButton.Size = new Size(75, 23);
            rotateSegmentButton.TabIndex = 11;
            rotateSegmentButton.Text = "Повернуть";
            rotateSegmentButton.UseVisualStyleBackColor = true;
            rotateSegmentButton.Click += rotateSegmentButton_Click;
            // 
            // rotatePolygonLabel
            // 
            rotatePolygonLabel.AutoSize = true;
            rotatePolygonLabel.Location = new Point(229, 9);
            rotatePolygonLabel.Name = "rotatePolygonLabel";
            rotatePolygonLabel.Size = new Size(111, 15);
            rotatePolygonLabel.TabIndex = 12;
            rotatePolygonLabel.Text = "Поворот полигона";
            // 
            // rotatePolygonCheckBox
            // 
            rotatePolygonCheckBox.AutoSize = true;
            rotatePolygonCheckBox.Location = new Point(231, 27);
            rotatePolygonCheckBox.Name = "rotatePolygonCheckBox";
            rotatePolygonCheckBox.Size = new Size(99, 19);
            rotatePolygonCheckBox.TabIndex = 13;
            rotatePolygonCheckBox.Text = "Вокруг точки";
            rotatePolygonCheckBox.UseVisualStyleBackColor = true;
            // 
            // rotatePolygonNumericUpDownAngle
            // 
            rotatePolygonNumericUpDownAngle.Location = new Point(229, 48);
            rotatePolygonNumericUpDownAngle.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            rotatePolygonNumericUpDownAngle.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            rotatePolygonNumericUpDownAngle.Name = "rotatePolygonNumericUpDownAngle";
            rotatePolygonNumericUpDownAngle.Size = new Size(75, 23);
            rotatePolygonNumericUpDownAngle.TabIndex = 27;
            // 
            // rotatePolygonButton
            // 
            rotatePolygonButton.Location = new Point(229, 77);
            rotatePolygonButton.Name = "rotatePolygonButton";
            rotatePolygonButton.Size = new Size(75, 23);
            rotatePolygonButton.TabIndex = 14;
            rotatePolygonButton.Text = "Повернуть";
            rotatePolygonButton.UseVisualStyleBackColor = true;
            rotatePolygonButton.Click += rotatePolygonButton_Click;
            // 
            // scalePolygonButton
            // 
            scalePolygonButton.Location = new Point(346, 111);
            scalePolygonButton.Name = "scalePolygonButton";
            scalePolygonButton.Size = new Size(116, 23);
            scalePolygonButton.TabIndex = 17;
            scalePolygonButton.Text = "Масштабировать";
            scalePolygonButton.UseVisualStyleBackColor = true;
            scalePolygonButton.Click += scalePolygonButton_Click;
            // 
            // scalePolygonCheckBox
            // 
            scalePolygonCheckBox.AutoSize = true;
            scalePolygonCheckBox.Location = new Point(348, 27);
            scalePolygonCheckBox.Name = "scalePolygonCheckBox";
            scalePolygonCheckBox.Size = new Size(140, 19);
            scalePolygonCheckBox.TabIndex = 16;
            scalePolygonCheckBox.Text = "Относительно точки";
            scalePolygonCheckBox.UseVisualStyleBackColor = true;
            // 
            // scalePolygonLabel
            // 
            scalePolygonLabel.AutoSize = true;
            scalePolygonLabel.Location = new Point(346, 9);
            scalePolygonLabel.Name = "scalePolygonLabel";
            scalePolygonLabel.Size = new Size(168, 15);
            scalePolygonLabel.TabIndex = 15;
            scalePolygonLabel.Text = "Масштабирование полигона";
            // 
            // scalePolygonNumericUpDownX
            // 
            scalePolygonNumericUpDownX.Location = new Point(348, 81);
            scalePolygonNumericUpDownX.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            scalePolygonNumericUpDownX.Minimum = new decimal(new int[] { 2, 0, 0, int.MinValue });
            scalePolygonNumericUpDownX.Name = "scalePolygonNumericUpDownX";
            scalePolygonNumericUpDownX.Size = new Size(75, 23);
            scalePolygonNumericUpDownX.TabIndex = 26;
            scalePolygonNumericUpDownX.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // scalePolygonNumericUpDownY
            // 
            scalePolygonNumericUpDownY.Location = new Point(348, 52);
            scalePolygonNumericUpDownY.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            scalePolygonNumericUpDownY.Minimum = new decimal(new int[] { 2, 0, 0, int.MinValue });
            scalePolygonNumericUpDownY.Name = "scalePolygonNumericUpDownY";
            scalePolygonNumericUpDownY.Size = new Size(75, 23);
            scalePolygonNumericUpDownY.TabIndex = 25;
            scalePolygonNumericUpDownY.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // findIntersectionsLabel
            // 
            findIntersectionsLabel.AutoSize = true;
            findIntersectionsLabel.Location = new Point(123, 146);
            findIntersectionsLabel.Name = "findIntersectionsLabel";
            findIntersectionsLabel.Size = new Size(181, 15);
            findIntersectionsLabel.TabIndex = 18;
            findIntersectionsLabel.Text = "Поиск точки пересчения ребер";
            // 
            // findIntersectionsButton
            // 
            findIntersectionsButton.Location = new Point(123, 165);
            findIntersectionsButton.Name = "findIntersectionsButton";
            findIntersectionsButton.Size = new Size(75, 23);
            findIntersectionsButton.TabIndex = 19;
            findIntersectionsButton.Text = "Найти";
            findIntersectionsButton.UseVisualStyleBackColor = true;
            findIntersectionsButton.Click += findIntersectionsButton_Click;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(10, 218);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1107, 457);
            pictureBox.TabIndex = 22;
            pictureBox.TabStop = false;
            pictureBox.Paint += PictureBox_Paint;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            // 
            // intersectionLabel
            // 
            intersectionLabel.AutoSize = true;
            intersectionLabel.Location = new Point(123, 191);
            intersectionLabel.Name = "intersectionLabel";
            intersectionLabel.Size = new Size(0, 15);
            intersectionLabel.TabIndex = 28;
            // 
            // pointInfo
            // 
            pointInfo.AutoSize = true;
            pointInfo.Location = new Point(346, 146);
            pointInfo.Name = "pointInfo";
            pointInfo.Size = new Size(280, 15);
            pointInfo.TabIndex = 29;
            pointInfo.Text = "Точка принадлежит выпуклому многоугольнику:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(346, 169);
            label1.Name = "label1";
            label1.Size = new Size(293, 15);
            label1.TabIndex = 30;
            label1.Text = "Точка принадлежит невыпуклому многоугольнику:";
            // 
            // belongsToConvex
            // 
            belongsToConvex.AutoSize = true;
            belongsToConvex.Location = new Point(639, 146);
            belongsToConvex.Name = "belongsToConvex";
            belongsToConvex.Size = new Size(0, 15);
            belongsToConvex.TabIndex = 31;
            // 
            // belongsToNConvex
            // 
            belongsToNConvex.AutoSize = true;
            belongsToNConvex.Location = new Point(645, 169);
            belongsToNConvex.Name = "belongsToNConvex";
            belongsToNConvex.Size = new Size(0, 15);
            belongsToNConvex.TabIndex = 32;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(346, 191);
            label2.Name = "label2";
            label2.Size = new Size(252, 15);
            label2.TabIndex = 33;
            label2.Text = "Расположение точки относительно отрезка:";
            // 
            // relativePointPosition
            // 
            relativePointPosition.AutoSize = true;
            relativePointPosition.Location = new Point(639, 191);
            relativePointPosition.Name = "relativePointPosition";
            relativePointPosition.Size = new Size(0, 15);
            relativePointPosition.TabIndex = 34;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1129, 687);
            Controls.Add(relativePointPosition);
            Controls.Add(label2);
            Controls.Add(belongsToNConvex);
            Controls.Add(belongsToConvex);
            Controls.Add(label1);
            Controls.Add(pointInfo);
            Controls.Add(intersectionLabel);
            Controls.Add(rotatePolygonNumericUpDownAngle);
            Controls.Add(scalePolygonNumericUpDownX);
            Controls.Add(scalePolygonNumericUpDownY);
            Controls.Add(movePolygonNumericUpDownY);
            Controls.Add(movePolygonNumericUpDownX);
            Controls.Add(pictureBox);
            Controls.Add(findIntersectionsButton);
            Controls.Add(findIntersectionsLabel);
            Controls.Add(scalePolygonButton);
            Controls.Add(scalePolygonCheckBox);
            Controls.Add(scalePolygonLabel);
            Controls.Add(rotatePolygonButton);
            Controls.Add(rotatePolygonCheckBox);
            Controls.Add(rotatePolygonLabel);
            Controls.Add(rotateSegmentButton);
            Controls.Add(rotateSegmentLabel);
            Controls.Add(movePolygonButton);
            Controls.Add(movePolygonLabel);
            Controls.Add(clearButton);
            Controls.Add(primitivesLabel);
            Controls.Add(polygonRadioButton);
            Controls.Add(segmentRadioButton);
            Controls.Add(pointRadioButton);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)movePolygonNumericUpDownX).EndInit();
            ((System.ComponentModel.ISupportInitialize)movePolygonNumericUpDownY).EndInit();
            ((System.ComponentModel.ISupportInitialize)rotatePolygonNumericUpDownAngle).EndInit();
            ((System.ComponentModel.ISupportInitialize)scalePolygonNumericUpDownX).EndInit();
            ((System.ComponentModel.ISupportInitialize)scalePolygonNumericUpDownY).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton pointRadioButton;
        private RadioButton segmentRadioButton;
        private RadioButton polygonRadioButton;
        private Label primitivesLabel;
        private Button clearButton;
        private Label movePolygonLabel;
        private Button movePolygonButton;
        private Label rotateSegmentLabel;
        private Button rotateSegmentButton;
        private Button rotatePolygonButton;
        private CheckBox rotatePolygonCheckBox;
        private Label rotatePolygonLabel;
        private Button scalePolygonButton;
        private CheckBox scalePolygonCheckBox;
        private Label scalePolygonLabel;
        private Label findIntersectionsLabel;
        private Button findIntersectionsButton;
        private PictureBox pictureBox;
        private NumericUpDown movePolygonNumericUpDownX;
        private NumericUpDown movePolygonNumericUpDownY;
        private NumericUpDown scalePolygonNumericUpDownX;
        private NumericUpDown scalePolygonNumericUpDownY;
        private NumericUpDown rotatePolygonNumericUpDownAngle;
        private Label intersectionLabel;
        private Label pointInfo;
        private Label label1;
        private Label belongsToConvex;
        private Label belongsToNConvex;
        private Label label2;
        private Label relativePointPosition;
    }
}