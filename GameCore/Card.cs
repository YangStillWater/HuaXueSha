using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore
{
    public abstract class Card
    {
        public abstract bool NeedTarget { get; }
        public virtual bool NeedResponse => true;
    }
    /// <summary>
    /// 基本牌
    /// </summary>
    public abstract class BaseCard : Card
    {
        public override bool NeedTarget => true;
    }
    /// <summary>
    /// 锦囊牌
    /// </summary>
    public abstract class KitCard : Card
    {
        public override bool NeedTarget => true;
        public override bool NeedResponse => true;
    }

    /// <summary>
    /// 装备
    /// </summary>
    public abstract class GearCard : Card
    {
        public override bool NeedTarget => false;
        public override bool NeedResponse => false;
    }

    /// <summary>
    /// 防具
    /// </summary>
    public abstract class ArmorCard : Card
    {
        public override bool NeedTarget => false;
    }

    /// <summary>
    /// 特殊牌
    /// </summary>
    public abstract class SpecialCard : Card
    {
        public override bool NeedTarget => true;
    }


    public class Acid : BaseCard { }
    public class Base : BaseCard { }
    public class Glucose : BaseCard
    {
        public override bool NeedResponse => false;
    }

    public class 重铬酸钾 : KitCard
    {
        public override bool NeedTarget => false;
    }

    public class 王水 : KitCard { }
    public class EDTA : KitCard { }
    public class 万碱齐发 : KitCard
    {
        public override bool NeedTarget => false;
    }

    public class 硫酸飞溅 : KitCard
    {
        public override bool NeedTarget => false;
    }

    public class 硅藻土 : KitCard { }
    public class 冷凝回流 : KitCard { }
    public class 负催化剂 : KitCard { }


    public class 试管 : GearCard { }
    public class pH计 : GearCard { }
    public class 酸式滴定管 : GearCard { }
    public class 碱式滴定管 : GearCard { }
    public class 锥形瓶 : GearCard { }
    public class 滴管 : GearCard { }
    public class 温度计 : GearCard { }

    public class 聚四氟乙烯 : ArmorCard { }
    public class 废液缸 : ArmorCard { }
    public class 玻璃塞 : ArmorCard { }

    public class 拉曼光谱 : SpecialCard { }
    public class 络合滴定 : SpecialCard { }

    public enum enumCard
    {
        //基本牌
        Acid,
        Base,
        Glucose,

        //锦囊牌
        重铬酸钾,
        王水,
        EDTA,
        万碱齐发,
        硫酸飞溅,
        硅藻土,
        冷凝回流,
        负催化剂,

        //装备
        试管,
        pH计,
        酸式滴定管,
        碱式滴定管,
        锥形瓶,
        滴管,
        温度计,

        //防具
        聚四氟乙烯,
        废液缸,
        玻璃塞,

        //特殊牌
        拉曼光谱,
        络合滴定,

    }
}
