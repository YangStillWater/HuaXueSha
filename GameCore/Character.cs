using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
public  abstract  class Character
    {
        public abstract string Introduction { get; }
        public virtual void Determine() { }
        public virtual void GetCards() { }
        public virtual void SelectOneCard() { }
        public virtual void SetTargets() { }
        public virtual void DealOneCard() { }
        public virtual void Respond() { }
        public virtual void DropCards() { }
    }

    public class 拉瓦锡 : Character
    {
        public override string Introduction => "技能1，物质守恒。在自己轮次外每失去一张牌，立即获得一张牌；技能2，命名。可将排面向下，当做任意即使牌使用，每回合限用一次。 ";
    }
    public class 门捷列夫 : Character
    {
        public override string Introduction => "技能1，预测（同观星）；技能2，周期表（改判定） ";
    }
    public class 路易斯 : Character
    {
        public override string Introduction => "酸碱理论：酸当碱，碱当酸 ";
    }
    public class 斯陶丁格 : Character
    {
        public override string Introduction => "1.聚合：任意手牌当做铁索连环，每回合限用一次；2.高分子：永远拥有PTFE ";
    }
    public class Soxhlet : Character
    {
        public override string Introduction => "索氏提取：任意方块牌当做“冷凝回流” ";
    }
    public class 居里夫人 : Character
    {
        public override string Introduction => "放射：受到伤害时，伤害来源者损失一点体力 ";
    }
    public class 维勒 : Character
    {
        public override string Introduction => "有机合成：任意两张牌可当葡萄糖使用 ";
    }
    public class 黄鸣龙 : Character
    {
        public override string Introduction => "还原：任意手牌可当做无懈可击 ";
    }
    public class 刘海洋 : Character
    {
        public override string Introduction => "使用“酸”或“硫酸飞溅”时，其他玩家需要使用两张碱 ";
    }
    public class 诺贝尔 : Character
    {
        public override string Introduction => "1.炸药：若放弃出牌阶段，可令自己于指定玩家同时弃掉所有手牌。2.颁奖：回合结束后，可摸一张牌 ";
    }
    public class 侯德榜 : Character
    {
        public override string Introduction => "侯氏制碱法：任意两张手牌可当“碱”使用，此“碱”无距离限制 ";
    }
    public class 舍勒 : Character
    {
        public override string Introduction => "燃烧论：任意红色牌可当“火攻”使用 ";
    }
    public class 卡文迪许 : Character
    {
        public override string Introduction => "1.惰性气体：火属性攻击不对其造成伤害；2.氢气：弃掉任意一张红色手牌可使下一次伤害+1 ";
    }
    public class 凯库勒 : Character
    {
        public override string Introduction => "1.咬尾蛇：可弃一张黑色牌，将伤害转给下家；2.苯环：弃掉两张手牌，立即改变游戏进行方向（原来逆时针，改为顺时针） ";
    }
    public class 薛定谔 : Character
    {
        public override string Introduction => "波函数：使用的酸或碱可指定攻击范围内2玩家 ";
    }
    public class 福井谦一 : Character
    {
        public override string Introduction => "前线轨道：永远同时拥有+1马和-1马 ";
    }
    public class 白川英树 : Character
    {
        public override string Introduction => "1.导电高分子：所有攻击附加电属性；2.可逆掺杂：收到“酸”或“硫酸飞溅”并损失体力后，将该牌置于角色旁，可免疫下一次的“酸”或“硫酸飞溅”，碱亦然 ";
    }
}
