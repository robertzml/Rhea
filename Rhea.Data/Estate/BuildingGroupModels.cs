using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Estate
{
    /// <summary>
    /// 建造方式
    /// </summary>
    public enum BuildType
    {
        自建 = 1,

        购入,

        接受捐赠,

        接受投资,

        融资相入
    }

    /// <summary>
    /// 建筑结构
    /// </summary>
    public enum BuildStructure
    {
        混合结构 = 1,

        砖木结构,

        砖石结构,

        木结构,

        钢混结构,

        钢结构,

        砖混结构,

        框架结构,

        彩钢结构,

        其他
    }

    /// <summary>
    /// 建筑物经费科目
    /// </summary>
    public enum FundsSubject
    {
        行政,

        事业,

        基建,

        社保,

        捐赠,

        外事,

        划拨,

        自筹,

        贷款,

        融资,

        其他
    }

    /// <summary>
    /// 建筑物房管形式
    /// </summary>
    public enum ManageType
    {
        自管 = 1,

        代管
    }
}
