namespace AudioRecord
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
            comboBoxMicrophones = new ComboBox();
            checkBoxListen = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // comboBoxMicrophones
            // 
            comboBoxMicrophones.FormattingEnabled = true;
            comboBoxMicrophones.Location = new Point(24, 27);
            comboBoxMicrophones.Name = "comboBoxMicrophones";
            comboBoxMicrophones.Size = new Size(297, 28);
            comboBoxMicrophones.TabIndex = 0;
            // 
            // checkBoxListen
            // 
            checkBoxListen.AutoSize = true;
            checkBoxListen.Location = new Point(327, 29);
            checkBoxListen.Name = "checkBoxListen";
            checkBoxListen.Size = new Size(61, 24);
            checkBoxListen.TabIndex = 1;
            checkBoxListen.Text = "监听";
            checkBoxListen.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(24, 61);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "开始录制";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(124, 61);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 3;
            button2.Text = "暂停录制";
            button2.UseVisualStyleBackColor = true;
            button2.KeyDown += Form1_KeyDown;
            button2.KeyUp += Form1_KeyUp;
            // 
            // button3
            // 
            button3.Location = new Point(227, 61);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 4;
            button3.Text = "结束并保存";
            button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 20F);
            label1.Location = new Point(24, 104);
            label1.Name = "label1";
            label1.Size = new Size(118, 45);
            label1.TabIndex = 6;
            label1.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 224);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(checkBoxListen);
            Controls.Add(comboBoxMicrophones);
            KeyPreview = true;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxMicrophones;
        private CheckBox checkBoxListen;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label1;
    }
}
