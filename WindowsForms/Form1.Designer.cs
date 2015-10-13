namespace WindowsForms
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbMessage = new System.Windows.Forms.ListBox();
            this.btnGameBegin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAvailableCount = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelDealCard = new System.Windows.Forms.Panel();
            this.bntFinishDeal = new System.Windows.Forms.Button();
            this.lblSelectTarget = new System.Windows.Forms.Label();
            this.lblSelectCard = new System.Windows.Forms.Label();
            this.btnDealCard = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDroppedCount = new System.Windows.Forms.Label();
            this.lblDefend = new System.Windows.Forms.Label();
            this.panelRespond = new System.Windows.Forms.Panel();
            this.btnTolerate = new System.Windows.Forms.Button();
            this.btnRespond = new System.Windows.Forms.Button();
            this.panelDrop = new System.Windows.Forms.Panel();
            this.btnDrop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panelDealCard.SuspendLayout();
            this.panelRespond.SuspendLayout();
            this.panelDrop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMessage
            // 
            this.lbMessage.FormattingEnabled = true;
            this.lbMessage.ItemHeight = 12;
            this.lbMessage.Location = new System.Drawing.Point(23, 12);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbMessage.Size = new System.Drawing.Size(601, 100);
            this.lbMessage.TabIndex = 0;
            // 
            // btnGameBegin
            // 
            this.btnGameBegin.Location = new System.Drawing.Point(660, 89);
            this.btnGameBegin.Name = "btnGameBegin";
            this.btnGameBegin.Size = new System.Drawing.Size(75, 23);
            this.btnGameBegin.TabIndex = 4;
            this.btnGameBegin.Text = "开始游戏";
            this.btnGameBegin.UseVisualStyleBackColor = true;
            this.btnGameBegin.Click += new System.EventHandler(this.btnGameBegin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(658, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "剩余牌数";
            // 
            // lblAvailableCount
            // 
            this.lblAvailableCount.AutoSize = true;
            this.lblAvailableCount.Location = new System.Drawing.Point(718, 32);
            this.lblAvailableCount.Name = "lblAvailableCount";
            this.lblAvailableCount.Size = new System.Drawing.Size(11, 12);
            this.lblAvailableCount.TabIndex = 6;
            this.lblAvailableCount.Text = "0";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(30, 153);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(594, 334);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // panelDealCard
            // 
            this.panelDealCard.Controls.Add(this.bntFinishDeal);
            this.panelDealCard.Controls.Add(this.lblSelectTarget);
            this.panelDealCard.Controls.Add(this.lblSelectCard);
            this.panelDealCard.Controls.Add(this.btnDealCard);
            this.panelDealCard.Location = new System.Drawing.Point(660, 153);
            this.panelDealCard.Name = "panelDealCard";
            this.panelDealCard.Size = new System.Drawing.Size(169, 100);
            this.panelDealCard.TabIndex = 9;
            this.panelDealCard.Visible = false;
            // 
            // bntFinishDeal
            // 
            this.bntFinishDeal.Location = new System.Drawing.Point(94, 70);
            this.bntFinishDeal.Name = "bntFinishDeal";
            this.bntFinishDeal.Size = new System.Drawing.Size(62, 23);
            this.bntFinishDeal.TabIndex = 2;
            this.bntFinishDeal.Text = "结束";
            this.bntFinishDeal.UseVisualStyleBackColor = true;
            this.bntFinishDeal.Click += new System.EventHandler(this.bntFinishDeal_Click);
            // 
            // lblSelectTarget
            // 
            this.lblSelectTarget.AutoSize = true;
            this.lblSelectTarget.Location = new System.Drawing.Point(13, 41);
            this.lblSelectTarget.Name = "lblSelectTarget";
            this.lblSelectTarget.Size = new System.Drawing.Size(65, 12);
            this.lblSelectTarget.TabIndex = 1;
            this.lblSelectTarget.Text = "请选择目标";
            this.lblSelectTarget.Visible = false;
            // 
            // lblSelectCard
            // 
            this.lblSelectCard.AutoSize = true;
            this.lblSelectCard.Location = new System.Drawing.Point(11, 15);
            this.lblSelectCard.Name = "lblSelectCard";
            this.lblSelectCard.Size = new System.Drawing.Size(77, 12);
            this.lblSelectCard.TabIndex = 1;
            this.lblSelectCard.Text = "请选择一张牌";
            // 
            // btnDealCard
            // 
            this.btnDealCard.Enabled = false;
            this.btnDealCard.Location = new System.Drawing.Point(13, 70);
            this.btnDealCard.Name = "btnDealCard";
            this.btnDealCard.Size = new System.Drawing.Size(62, 23);
            this.btnDealCard.TabIndex = 0;
            this.btnDealCard.Text = "出牌";
            this.btnDealCard.UseVisualStyleBackColor = true;
            this.btnDealCard.Click += new System.EventHandler(this.btnDealCard_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(658, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "弃牌堆牌数";
            // 
            // lblDroppedCount
            // 
            this.lblDroppedCount.AutoSize = true;
            this.lblDroppedCount.Location = new System.Drawing.Point(731, 59);
            this.lblDroppedCount.Name = "lblDroppedCount";
            this.lblDroppedCount.Size = new System.Drawing.Size(11, 12);
            this.lblDroppedCount.TabIndex = 11;
            this.lblDroppedCount.Text = "0";
            // 
            // lblDefend
            // 
            this.lblDefend.AutoSize = true;
            this.lblDefend.Location = new System.Drawing.Point(10, 16);
            this.lblDefend.Name = "lblDefend";
            this.lblDefend.Size = new System.Drawing.Size(65, 12);
            this.lblDefend.TabIndex = 12;
            this.lblDefend.Text = "请出牌应对";
            // 
            // panelRespond
            // 
            this.panelRespond.Controls.Add(this.btnTolerate);
            this.panelRespond.Controls.Add(this.btnRespond);
            this.panelRespond.Controls.Add(this.lblDefend);
            this.panelRespond.Location = new System.Drawing.Point(660, 272);
            this.panelRespond.Name = "panelRespond";
            this.panelRespond.Size = new System.Drawing.Size(169, 83);
            this.panelRespond.TabIndex = 13;
            this.panelRespond.Visible = false;
            // 
            // btnTolerate
            // 
            this.btnTolerate.Location = new System.Drawing.Point(94, 43);
            this.btnTolerate.Name = "btnTolerate";
            this.btnTolerate.Size = new System.Drawing.Size(62, 23);
            this.btnTolerate.TabIndex = 14;
            this.btnTolerate.Text = "放弃";
            this.btnTolerate.UseVisualStyleBackColor = true;
            this.btnTolerate.Click += new System.EventHandler(this.btnTolerate_Click);
            // 
            // btnRespond
            // 
            this.btnRespond.Location = new System.Drawing.Point(13, 43);
            this.btnRespond.Name = "btnRespond";
            this.btnRespond.Size = new System.Drawing.Size(62, 23);
            this.btnRespond.TabIndex = 13;
            this.btnRespond.Text = "出牌";
            this.btnRespond.UseVisualStyleBackColor = true;
            this.btnRespond.Click += new System.EventHandler(this.btnRespond_Click);
            // 
            // panelDrop
            // 
            this.panelDrop.Controls.Add(this.btnDrop);
            this.panelDrop.Controls.Add(this.label2);
            this.panelDrop.Location = new System.Drawing.Point(660, 382);
            this.panelDrop.Name = "panelDrop";
            this.panelDrop.Size = new System.Drawing.Size(168, 104);
            this.panelDrop.TabIndex = 14;
            this.panelDrop.Visible = false;
            // 
            // btnDrop
            // 
            this.btnDrop.Location = new System.Drawing.Point(13, 60);
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.Size = new System.Drawing.Size(62, 23);
            this.btnDrop.TabIndex = 14;
            this.btnDrop.Text = "弃牌";
            this.btnDrop.UseVisualStyleBackColor = true;
            this.btnDrop.Click += new System.EventHandler(this.btnDrop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "请选择弃掉的牌";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 522);
            this.Controls.Add(this.panelDrop);
            this.Controls.Add(this.panelRespond);
            this.Controls.Add(this.lblDroppedCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panelDealCard);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblAvailableCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGameBegin);
            this.Controls.Add(this.lbMessage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panelDealCard.ResumeLayout(false);
            this.panelDealCard.PerformLayout();
            this.panelRespond.ResumeLayout(false);
            this.panelRespond.PerformLayout();
            this.panelDrop.ResumeLayout(false);
            this.panelDrop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbMessage;
        private System.Windows.Forms.Button btnGameBegin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAvailableCount;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelDealCard;
        private System.Windows.Forms.Label lblSelectCard;
        private System.Windows.Forms.Button btnDealCard;
        private System.Windows.Forms.Label lblSelectTarget;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDroppedCount;
        private System.Windows.Forms.Label lblDefend;
        private System.Windows.Forms.Button bntFinishDeal;
        private System.Windows.Forms.Panel panelRespond;
        private System.Windows.Forms.Button btnTolerate;
        private System.Windows.Forms.Button btnRespond;
        private System.Windows.Forms.Panel panelDrop;
        private System.Windows.Forms.Button btnDrop;
        private System.Windows.Forms.Label label2;
    }
}

