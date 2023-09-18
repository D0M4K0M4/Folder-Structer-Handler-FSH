namespace FSH_filter
{
    partial class fsh_filter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fsh_filter));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cre_by = new System.Windows.Forms.Label();
            this.ok_bt = new System.Windows.Forms.Button();
            this.line_p = new System.Windows.Forms.PictureBox();
            this.file_type_tb = new System.Windows.Forms.TextBox();
            this.file_contain_tb = new System.Windows.Forms.TextBox();
            this.file_type_bt = new System.Windows.Forms.Button();
            this.file_contain_bt = new System.Windows.Forms.Button();
            this.file_type_l = new System.Windows.Forms.Label();
            this.file_contain_l = new System.Windows.Forms.Label();
            this.inf_type_l = new System.Windows.Forms.Label();
            this.inf_contain_l = new System.Windows.Forms.Label();
            this.file_type_bx = new System.Windows.Forms.TextBox();
            this.file_cont_bx = new System.Windows.Forms.TextBox();
            this.hide_p = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.line_p)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hide_p)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(15, 181);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // cre_by
            // 
            this.cre_by.AutoSize = true;
            this.cre_by.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.cre_by.Location = new System.Drawing.Point(38, 185);
            this.cre_by.Name = "cre_by";
            this.cre_by.Size = new System.Drawing.Size(461, 14);
            this.cre_by.TabIndex = 1;
            this.cre_by.Text = "Folder Structer Handler v0.0.1 - Copyright © 2023 by D0M4K0M4. All Rights Reserve" +
    "d";
            // 
            // ok_bt
            // 
            this.ok_bt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ok_bt.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.ok_bt.Location = new System.Drawing.Point(574, 175);
            this.ok_bt.Name = "ok_bt";
            this.ok_bt.Size = new System.Drawing.Size(86, 26);
            this.ok_bt.TabIndex = 2;
            this.ok_bt.Text = "OK";
            this.ok_bt.UseVisualStyleBackColor = true;
            this.ok_bt.Click += new System.EventHandler(this.ok_bt_Click);
            // 
            // line_p
            // 
            this.line_p.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("line_p.BackgroundImage")));
            this.line_p.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.line_p.Location = new System.Drawing.Point(12, 84);
            this.line_p.Name = "line_p";
            this.line_p.Size = new System.Drawing.Size(548, 10);
            this.line_p.TabIndex = 3;
            this.line_p.TabStop = false;
            // 
            // file_type_tb
            // 
            this.file_type_tb.BackColor = System.Drawing.SystemColors.Window;
            this.file_type_tb.Location = new System.Drawing.Point(12, 35);
            this.file_type_tb.Name = "file_type_tb";
            this.file_type_tb.Size = new System.Drawing.Size(456, 20);
            this.file_type_tb.TabIndex = 4;
            // 
            // file_contain_tb
            // 
            this.file_contain_tb.Location = new System.Drawing.Point(12, 122);
            this.file_contain_tb.Name = "file_contain_tb";
            this.file_contain_tb.Size = new System.Drawing.Size(456, 20);
            this.file_contain_tb.TabIndex = 5;
            // 
            // file_type_bt
            // 
            this.file_type_bt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.file_type_bt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.file_type_bt.Location = new System.Drawing.Point(474, 33);
            this.file_type_bt.Name = "file_type_bt";
            this.file_type_bt.Size = new System.Drawing.Size(86, 23);
            this.file_type_bt.TabIndex = 6;
            this.file_type_bt.Text = "SAVE";
            this.file_type_bt.UseVisualStyleBackColor = true;
            this.file_type_bt.Click += new System.EventHandler(this.file_type_bt_Click);
            // 
            // file_contain_bt
            // 
            this.file_contain_bt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.file_contain_bt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.file_contain_bt.Location = new System.Drawing.Point(474, 120);
            this.file_contain_bt.Name = "file_contain_bt";
            this.file_contain_bt.Size = new System.Drawing.Size(86, 23);
            this.file_contain_bt.TabIndex = 7;
            this.file_contain_bt.Text = "SAVE";
            this.file_contain_bt.UseVisualStyleBackColor = true;
            this.file_contain_bt.Click += new System.EventHandler(this.file_contain_bt_Click);
            // 
            // file_type_l
            // 
            this.file_type_l.AutoSize = true;
            this.file_type_l.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.file_type_l.Location = new System.Drawing.Point(9, 18);
            this.file_type_l.Name = "file_type_l";
            this.file_type_l.Size = new System.Drawing.Size(519, 14);
            this.file_type_l.TabIndex = 8;
            this.file_type_l.Text = "File extension filtering: (separate extensions with \',\') - if it is empty it will" +
    " be no filtering apply";
            // 
            // file_contain_l
            // 
            this.file_contain_l.AutoSize = true;
            this.file_contain_l.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.file_contain_l.Location = new System.Drawing.Point(9, 105);
            this.file_contain_l.Name = "file_contain_l";
            this.file_contain_l.Size = new System.Drawing.Size(504, 14);
            this.file_contain_l.TabIndex = 9;
            this.file_contain_l.Text = "File contain filtering: (separate extensions with \',\') - if it is empty it will b" +
    "e no filtering apply";
            // 
            // inf_type_l
            // 
            this.inf_type_l.AutoSize = true;
            this.inf_type_l.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.inf_type_l.Location = new System.Drawing.Point(9, 58);
            this.inf_type_l.Name = "inf_type_l";
            this.inf_type_l.Size = new System.Drawing.Size(551, 14);
            this.inf_type_l.TabIndex = 10;
            this.inf_type_l.Text = "Write down some extension type(s) for filtering: In this example: .png, .txt, .jp" +
    "g only this file types will be handled.";
            // 
            // inf_contain_l
            // 
            this.inf_contain_l.AutoSize = true;
            this.inf_contain_l.Font = new System.Drawing.Font("Arial", 8.25F);
            this.inf_contain_l.Location = new System.Drawing.Point(9, 145);
            this.inf_contain_l.Name = "inf_contain_l";
            this.inf_contain_l.Size = new System.Drawing.Size(646, 14);
            this.inf_contain_l.TabIndex = 11;
            this.inf_contain_l.Text = "Write down some text components for filtering: In this example: car, 1, dog only " +
    "files handled that is contains one of this components.";
            // 
            // file_type_bx
            // 
            this.file_type_bx.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.file_type_bx.Location = new System.Drawing.Point(574, 18);
            this.file_type_bx.Multiline = true;
            this.file_type_bx.Name = "file_type_bx";
            this.file_type_bx.ReadOnly = true;
            this.file_type_bx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.file_type_bx.Size = new System.Drawing.Size(86, 54);
            this.file_type_bx.TabIndex = 12;
            this.file_type_bx.TextChanged += new System.EventHandler(this.file_type_bx_TextChanged);
            // 
            // file_cont_bx
            // 
            this.file_cont_bx.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.file_cont_bx.Location = new System.Drawing.Point(574, 84);
            this.file_cont_bx.Multiline = true;
            this.file_cont_bx.Name = "file_cont_bx";
            this.file_cont_bx.ReadOnly = true;
            this.file_cont_bx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.file_cont_bx.Size = new System.Drawing.Size(86, 58);
            this.file_cont_bx.TabIndex = 13;
            this.file_cont_bx.TextChanged += new System.EventHandler(this.file_cont_bx_TextChanged);
            // 
            // hide_p
            // 
            this.hide_p.Location = new System.Drawing.Point(642, 18);
            this.hide_p.Name = "hide_p";
            this.hide_p.Size = new System.Drawing.Size(18, 124);
            this.hide_p.TabIndex = 14;
            this.hide_p.TabStop = false;
            // 
            // fsh_filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 213);
            this.Controls.Add(this.hide_p);
            this.Controls.Add(this.file_cont_bx);
            this.Controls.Add(this.file_type_bx);
            this.Controls.Add(this.inf_contain_l);
            this.Controls.Add(this.inf_type_l);
            this.Controls.Add(this.file_contain_l);
            this.Controls.Add(this.file_type_l);
            this.Controls.Add(this.file_contain_bt);
            this.Controls.Add(this.file_type_bt);
            this.Controls.Add(this.file_contain_tb);
            this.Controls.Add(this.file_type_tb);
            this.Controls.Add(this.line_p);
            this.Controls.Add(this.ok_bt);
            this.Controls.Add(this.cre_by);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fsh_filter";
            this.Text = "FSH";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.line_p)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hide_p)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label cre_by;
        private System.Windows.Forms.Button ok_bt;
        private System.Windows.Forms.PictureBox line_p;
        private System.Windows.Forms.TextBox file_type_tb;
        private System.Windows.Forms.TextBox file_contain_tb;
        private System.Windows.Forms.Button file_type_bt;
        private System.Windows.Forms.Button file_contain_bt;
        private System.Windows.Forms.Label file_type_l;
        private System.Windows.Forms.Label file_contain_l;
        private System.Windows.Forms.Label inf_type_l;
        private System.Windows.Forms.Label inf_contain_l;
        private System.Windows.Forms.TextBox file_type_bx;
        private System.Windows.Forms.TextBox file_cont_bx;
        private System.Windows.Forms.PictureBox hide_p;
    }
}

