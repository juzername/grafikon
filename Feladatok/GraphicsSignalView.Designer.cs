namespace Signals
{
    partial class GraphicsSignalView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.zoomInB = new System.Windows.Forms.Button();
            this.zoomOutB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zoomInB
            // 
            this.zoomInB.Location = new System.Drawing.Point(19, 18);
            this.zoomInB.Name = "zoomInB";
            this.zoomInB.Size = new System.Drawing.Size(28, 23);
            this.zoomInB.TabIndex = 0;
            this.zoomInB.Text = "+";
            this.zoomInB.UseVisualStyleBackColor = true;
            this.zoomInB.Click += new System.EventHandler(this.zoomInB_Click);
            // 
            // zoomOutB
            // 
            this.zoomOutB.Location = new System.Drawing.Point(53, 18);
            this.zoomOutB.Name = "zoomOutB";
            this.zoomOutB.Size = new System.Drawing.Size(29, 23);
            this.zoomOutB.TabIndex = 1;
            this.zoomOutB.Text = "-";
            this.zoomOutB.UseVisualStyleBackColor = true;
            this.zoomOutB.Click += new System.EventHandler(this.zoomOutB_Click);
            // 
            // GraphicsSignalView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zoomOutB);
            this.Controls.Add(this.zoomInB);
            this.Name = "GraphicsSignalView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button zoomInB;
        private System.Windows.Forms.Button zoomOutB;
    }
}
