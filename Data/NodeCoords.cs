using System.Linq;
using GatherBuddy.Classes;
using GatherBuddy.Enums;
using GatherBuddy.Game;
using GatherBuddy.Managers;
using GatherBuddy.Nodes;

namespace GatherBuddy.Data
{
    public static class NodeCoords
    {
        public static void SetCoords(Node node, AetheryteManager aetherytes) {
            bool Apply(string zone, string item, double x, double y, string aetheryte, bool flag = false) {
                if (zone != node.PlaceNameEn || !node.Items!.HasItems(item) || node.InitialPos != null)
                    return false;

                node.InitialPos = new InitialNodePosition() {
                    XCoordIntegral = (int)(x * 100.0 + 0.9),
                    YCoordIntegral = (int)(y * 100.0 + 0.9),
                    Prefer = flag,
                    ClosestAetheryte = aetherytes.Aetherytes.FirstOrDefault(a => a.Name == aetheryte),
                };
                return true;
            }

            // Diadem special
            if (node.Nodes!.Territory!.Name == "云冠群岛") {
                node.InitialPos = new InitialNodePosition() {
                    XCoordIntegral = 0,
                    YCoordIntegral = 0,
                    Prefer = true,
                    ClosestAetheryte = aetherytes.Aetherytes.First(a => a.Name == "伊修加德基础层"),
                };
                return;
            }

            // @formatter:off
            if (node.Meta!.NodeType == NodeType.Ephemeral)
                switch (node.Meta.Level + node.Meta.GatheringType) {
                    // Shadowbringers
                    case 80 + GatheringType.Harvesting:
                        if (Apply("杂草岛", "沼泽鼠尾草", 25.2, 29.8, "乔布要塞")) return;
                        if (Apply("野园", "甜墨角兰", 26.9, 24.3, "法诺村")) return;
                        if (Apply("碎石山地", "白玉土", 13.6, 13.8, "图姆拉村")) return;
                        break;
                    case 80 + GatheringType.Mining:
                        if (Apply("野园", "暴风岩", 22.3, 18.3, "蛇行枝")) return;
                        if (Apply("光耀教会", "光耀石", 36.8, 14.9, "乔布要塞")) return;
                        break;
                    case 80 + GatheringType.Quarrying:
                        if (Apply("友好村", "轰雷性岩", 22.3, 18.3, "图姆拉村")) return;
                        break;

                    // Stormblood
                    case 70 + GatheringType.Harvesting:
                        if (Apply("多玛", "风茶叶", 22.0, 13.0, "烈士庵")) return;
                        if (Apply("月神沙漠北端", "黄土", 15.0, 29.0, "朵洛衣楼")) return;
                        break;
                    case 70 + GatheringType.Logging:
                        if (Apply("阿巴拉提亚龙头", "榧树枝", 28.0, 10.0, "天营门")) return;
                        break;
                    case 70 + GatheringType.Mining:
                        if (Apply("高岸", "贵榴石", 13.0, 17.0, "天营门")) return;
                        if (Apply("昂萨哈凯尔", "黑碧玺", 29.0, 15.0, "晨曦王座")) return;
                        break;
                    case 70 + GatheringType.Quarrying:
                        if (Apply("祖灵笑", "珍珠岩", 37.0, 19.0, "茨菰村水塘")) return;
                        break;

                    // Heavensward
                    case 60 + GatheringType.Harvesting:
                        if (Apply("红沿", "牛至", 16.0, 32.0, "隼巢")) return;
                        if (Apply("阿瓦隆尼亚古陆", "赤玉土", 10.0, 32.0, "不洁三塔")) return;
                        if (Apply("绿茵岛", "赤玉土", 20.0, 30.0, "天极白垩宫")) return;
                        if (Apply("莫克温杜集落", "牛至", 23.0, 12.0, "尊杜集落")) return;
                        if (Apply("圣茉夏娜植物园", "赤玉土", 13.0, 19.0, "田园郡")) return;
                        break;
                    case 60 + GatheringType.Quarrying:
                        if (Apply("黑铁大桥", "雷砂砾", 21.0, 28.0, "隼巢")) return;
                        if (Apply("阿瓦隆尼亚古陆", "火砂砾", 17.0, 27.0, "不洁三塔")) return;
                        if (Apply("萨雷安精制区", "火砂砾", 26.0, 24.0, "田园郡")) return;
                        if (Apply("人王像", "火砂砾", 29.0, 19.0, "莫古力之家")) return;
                        if (Apply("衮杜集落", "雷砂砾", 34.0, 30.0, "云顶营地")) return;
                        break;
                }

            if (node.Meta!.NodeType == NodeType.Unspoiled)
                switch (node.Meta.Level + node.Meta.GatheringType) {
                    case 80 + GatheringType.Harvesting:
                        if (Apply("尾之道", "黄新薯", 19.0, 16.0, "鼹灵集市")) return;
                        if (Apply("射手露宿地", "迷雾菠菜", 34.0, 21.0, "法诺村")) return;
                        if (Apply("无垢之证", "泡茧", 27.0, 10.0, "乔布要塞")) return;
                        if (Apply("白油瀑布", "柔嫩茴香", 28.4, 21.1, "滞潮村", true)) return;
                        if (Apply("拿巴示断绝", "紫草", 32.2, 33.4, "上路客店", true)) return;
                        if (Apply("快婿树岛", "苍棉花", 33.7, 13.2, "法诺村", true)) return;
                        break;
                    case 80 + GatheringType.Logging:
                        if (Apply("刺舌滴", "小柠檬", 20.0, 27.0, "工匠村")) return;
                        if (Apply("缪栎的乡愁", "檀香木原木", 24.0, 36.0, "蛇行枝")) return;
                        if (Apply("安登小羊圈", "印茄原木", 36.5, 27.3, "群花馆", true)) return;
                        if (Apply("迦利克村", "银山毛榉原木", 16.0, 10.9, "络尾聚落", true)) return;
                        if (Apply("责罚监狱", "硬质金合欢树皮", 5.2, 26.5, "奥斯塔尔严命城", true)) return;
                        if (Apply("中琥珀丘", "收藏用沙柚木原木", 18.6, 20.5, "络尾聚落")) return;
                        if (Apply("诺弗兰特大陆坡", "收藏用海藻", 37.5, 11.7, "鳍人潮池")) return;
                        if (Apply("石楠瀑布", "收藏用乌仑代硬木原木", 30.6, 6.9, "阿拉加纳")) return;
                        break;
                    case 80 + GatheringType.Mining:
                        if (Apply("水站", "三重石原石", 20.0, 29.0, "上路客店")) return;
                        if (Apply("杂草岛", "透锂长石原石", 28.0, 33.0, "乔布要塞")) return;
                        if (Apply("卡利班深海峡", "黑玛瑙原石", 16.0, 21.0, "马克连萨斯广场")) return;
                        if (Apply("圣法斯里克天庭", "彩虹晶", 30.0, 20.0, "云村")) return;
                        if (Apply("陆人墓标", "重钨矿", 32.0, 7.0, "鳍人潮池")) return;
                        if (Apply("贤岛", "金锡矿", 4.7, 33.9, "奥斯塔尔严命城", true)) return;
                        if (Apply("破裂卵壳", "苦灰石", 7.5, 30.1, "蛇行枝", true)) return;
                        if (Apply("侏儒烟囱", "超硬水", 36.1, 12.2, "图姆拉村", true)) return;
                        if (Apply("鳍人潮池", "收藏用海底岩", 32.7, 20.6, "鳍人潮池")) return;
                        if (Apply("诺弗兰特大陆坡", "收藏用海盐", 25.1, 4.5, "鳍人潮池")) return;
                        if (Apply("无垢干谷", "收藏用基拉巴尼亚明矾", 31.6, 31.4, "对等石")) return;
                        break;
                    case 80 + GatheringType.Quarrying:
                        if (Apply("尊紫洞", "尊紫洞 Shell Chip", 34.4, 31.3, "马克连萨斯广场", true)) return;
                        if (Apply("比朗大矿山", "灰明矾", 20.1, 8.6, "络尾聚落", true)) return;
                        if (Apply("孚布特堡", "白夜结晶", 35.6, 8.7, "云村", true)) return;
                        break;

                    case 75 + GatheringType.Logging:
                        if (Apply("凿岩门", "收藏用白橡木原木", 28.0, 32.8, "滞潮村")) return;
                        if (Apply("羊毛道", "收藏用仙子苹果", 4.1, 23.1, "群花馆")) return;
                        break;
                    case 75 + GatheringType.Harvesting:
                        if (Apply("群树馆", "蚕豆", 24.0, 36.0, "群花馆")) return;
                        if (Apply("射孔", "欧薄荷", 26.0, 20.0, "乔布要塞")) return;
                        break;
                    case 75 + GatheringType.Mining:
                        if (Apply("六子浅滩", "硬水铝石原石", 26.0, 13.0, "云村")) return;
                        if (Apply("书著者树洞", "青金石原石", 25.0, 34.0, "蛇行枝")) return;
                        if (Apply("束带路", "收藏用钛铜矿", 31.3, 24.2, "乔布要塞")) return;
                        if (Apply("书著者树洞", "收藏用青金石原石", 16.5, 18.2, "蛇行枝")) return;
                        break;

                    case 70 + GatheringType.Harvesting:
                        if (Apply("红锈岩山", "祝圣罗勒草", 23.0, 16.0, "阿拉加纳")) return;
                        if (Apply("七彩溪谷", "莲藕", 28.0, 7.0, "烈士庵")) return;
                        if (Apply("阿巴拉提亚龙头", "长颈骆姜", 8.0, 8.0, "天营门")) return;
                        if (Apply("昂萨哈凯尔", "苎麻", 20.0, 8.0, "晨曦王座")) return;
                        if (Apply("约恩山", "真麻", 24.0, 36.0, "阿拉基利")) return;
                        if (Apply("无二江流域", "延夏棉", 28.0, 35.0, "茨菰村水塘")) return;
                        break;
                    case 70 + GatheringType.Logging:
                        if (Apply("昂萨哈凯尔", "奥萨德梅", 27.0, 17.0, "晨曦王座")) return;
                        if (Apply("高岸", "榧木原木", 11.0, 13.0, "天营门")) return;
                        if (Apply("尖枪瀑布", "黑柳原木", 15.0, 21.0, "帝国东方堡")) return;
                        if (Apply("红锈岩山", "乌仑代硬木原木", 32.0, 10.0, "阿拉加纳")) return;
                        if (Apply("温蒙特造船厂", "白橡木枝", 12.0, 29.0, "工匠村")) return;
                        if (Apply("多玛", "收藏用松脂", 18.7, 14.4, "烈士庵")) return;
                        break;
                    case 70 + GatheringType.Mining:
                        if (Apply("红锈岩山", "蔷薇辉石原石", 26.0, 12.0, "阿拉加纳")) return;
                        if (Apply("七彩溪谷", "琅玕原石", 29.0, 9.0, "烈士庵")) return;
                        if (Apply("挡风巨岩", "石青原石", 5.0, 29.0, "朵洛衣楼")) return;
                        if (Apply("白鬼岩山", "铬铁矿", 16.0, 33.9, "阿拉基利")) return;
                        if (Apply("多玛", "钯金矿", 20.5, 10.4, "烈士庵")) return;
                        if (Apply("奥萨德东岸", "夜铁矿", 11.0, 23.0, "自凝岛近海")) return;
                        if (Apply("月神沙漠北端", "清银矿", 23.0, 36.0, "重逢集市")) return;
                        if (Apply("无垢干谷", "岩铁", 31.0, 27.0, "对等石")) return;
                        if (Apply("盐湖", "常辉矿", 22.0, 13.0, "天营门")) return;
                        if (Apply("总督田地", "乌钢石原石", 33.0, 23.0, "滞潮村")) return;
                        if (Apply("刃海", "收藏用石青原石", 36.0, 26.4, "重逢集市")) return;
                        break;
                    case 70 + GatheringType.Quarrying:
                        if (Apply("盐湖", "阿拉米格盐", 21.0, 29.0, "阿拉米格人居住区")) return;
                        break;

                    case 65 + GatheringType.Logging:
                        if (Apply("无二江流域", "竹笋", 28.0, 25.0, "茨菰村水塘")) return;
                        if (Apply("渔村沿岸", "收藏用落叶松原木", 6.0, 15.8, "自凝岛近海")) return;
                        if (Apply("冲之岩近海", "收藏用香菇", 33.1, 9.2, "自凝岛近海", true)) return; // Aetheryte
                        break;
                    case 65 + GatheringType.Mining:
                        if (Apply("奥萨德东岸", "星尖石原石", 15.0, 4.5, "自凝岛近海")) return;
                        if (Apply("幻河", "收藏用锂辉石原石", 29.6, 12.9, "神拳痕", true)) return; // Aetheryte
                        if (Apply("尖枪瀑布", "收藏用基拉巴尼亚矿泉水", 18.1, 22.8, "对等石")) return;
                        if (Apply("蟹茹滨", "收藏用星尖石原石", 21.2, 34.4, "黄金港城区", true)) return; // Aetheryte
                        break;

                    case 60 + GatheringType.Harvesting:
                        if (Apply("德尔塔管区", "星棉", 9.0, 31.0, "螺旋港")) return;
                        if (Apply("陆行鸟之森", "显贵鼠尾草", 33.0, 30.0, "尾羽集落")) return;
                        if (Apply("双子池", "卡贝基野菜", 8.0, 9.0, "尾羽集落")) return;
                        if (Apply("双子池", "香草豆", 23.0, 21.0, "隼巢")) return;
                        if (Apply("绿茵岛", "星极花", 17.0, 36.0, "天极白垩宫")) return;
                        if (Apply("萨雷安精制区", "鲜红罗兰莓", 39.0, 26.0, "不洁三塔")) return;
                        break;
                    case 60 + GatheringType.Logging:
                        if (Apply("萨雷安治学区", "柚木原木", 6.0, 28.0, "田园郡")) return;
                        if (Apply("绿茵岛", "棕菇", 12.0, 37.0, "天极白垩宫")) return;
                        if (Apply("温杜属本杜集落", "伊修加德柠檬", 35.0, 23.0, "云顶营地")) return;
                        if (Apply("萨雷安睿哲区", "云海香蕉", 19.0, 36.0, "田园郡")) return;
                        if (Apply("试炼群岛", "甜扁桃", 24.0, 6.0, "尊杜集落")) return;
                        if (Apply("贝塔管区", "金合欢树皮", 22.0, 10.0, "螺旋港")) return;
                        if (Apply("竖骨岛", "樟木古树", 11.0, 10.0, "天极白垩宫")) return;
                        if (Apply("东境混交林", "山毛榉树枝", 11.0, 18.0, "帝国东方堡")) return;
                        if (Apply("阿瓦隆尼亚古陆", "收藏用毛栗子", 16.2, 35.5, "不洁三塔")) return;
                        break;
                    case 60 + GatheringType.Mining:
                        if (Apply("双子池", "钨华", 10.0, 9.0, "尾羽集落")) return;
                        if (Apply("阿尔法管区", "辉金矿", 5.0, 17.0, "螺旋港")) return;
                        if (Apply("萨雷安睿哲区", "赤铁矿", 34.0, 30.0, "不洁三塔")) return;
                        if (Apply("贝塔管区", "精金矿", 24.0, 6.0, "螺旋港")) return;
                        if (Apply("绿茵岛", "皇金矿", 11.0, 38.0, "天极白垩宫")) return;
                        if (Apply("生态园", "红明矾", 35.0, 16.0, "螺旋港")) return;
                        if (Apply("试炼群岛", "菱锌矿", 38.0, 15.0, "尊杜集落")) return;
                        if (Apply("条纹丘", "锂辉石原石", 25.0, 8.0, "帝国东方堡")) return;
                        if (Apply("蓝天窗", "收藏用阿巴拉提亚天然水", 20.6, 11.6, "尊杜集落")) return;
                        break;
                    case 60 + GatheringType.Quarrying:
                        if (Apply("萨雷安睿哲区", "沸石", 13.0, 31.0, "田园郡")) return;
                        if (Apply("试炼群岛", "阿巴拉提亚岩盐", 7.0, 7.0, "尊杜集落")) return;
                        if (Apply("交汇河", "星砂砾", 37.0, 16.0, "隼巢")) return;
                        if (Apply("沃仙曦染", "金云母", 35.0, 39.0, "云顶营地")) return;
                        if (Apply("萨雷安治学区", "绀碧石英", 11.0, 16.0, "田园郡")) return;
                        break;
                    case 55 + GatheringType.Harvesting:
                        if (Apply("交汇河", "小包心菜", 31.0, 20.0, "隼巢")) return;
                        break;
                    case 55 + GatheringType.Logging:
                        if (Apply("竖骨岛", "牛肝菌", 24.0, 6.0, "天极白垩宫")) return;
                        if (Apply("荒烟野地", "收藏用暗栗木树液", 29.2, 30.1, "尾羽集落")) return;
                        break;
                    case 55 + GatheringType.Mining:
                        if (Apply("荒烟野地", "黄铁矿", 26.0, 17.0, "尾羽集落")) return;
                        if (Apply("荒烟野地", "收藏用黄铁矿", 30.7, 32.2, "尾羽集落")) return;
                        break;
                    case 55 + GatheringType.Quarrying:
                        if (Apply("招恶荒岛", "翠绿石英", 33.0, 22.0, "莫古力之家")) return;
                        break;
                    case 50 + GatheringType.Harvesting:
                        if (Apply("九藤", "蚕茧", 22.0, 26.0, "霍桑山寨")) return;
                        if (Apply("荆棘森", "延龄花", 17.0, 19.0, "霍桑山寨")) return;
                        if (Apply("蜜场", "延龄草的球根", 13.0, 23.0, "霍桑山寨")) return;
                        if (Apply("鲜血滨", "蜜柠檬", 26.0, 32.0, "太阳海岸")) return;
                        if (Apply("接雨草树林", "泽梅尔番茄", 18.0, 26.0, "葡萄酒港")) return;
                        if (Apply("枯骨", "黑松露", 12.0, 16.0, "Camp 枯骨")) return;
                        if (Apply("酸模避风港", "提诺尔卡茶叶", 16.0, 20.0, "弯枝牧场")) return;
                        if (Apply("四分石地", "拉诺西亚韭菜", 34.0, 28.0, "雨燕塔殖民地")) return;
                        if (Apply("雪松原", "谢尔达莱嫩菠菜", 32.0, 11.0, "莫拉比造船厂")) return;
                        if (Apply("九藤", "迷迭香", 22.0, 30.0, "霍桑山寨")) return;
                        if (Apply("陆行鸟之森", "旧世界无花果", 26.0, 12.0, "尾羽集落")) return;
                        break;
                    case 50 + GatheringType.Logging:
                        if (Apply("神意之地", "云杉原木", 27.0, 12.0, "巨龙首营地")) return;
                        if (Apply("巨龙首", "吸血枝", 27.0, 24.0, "Camp 巨龙首")) return;
                        if (Apply("白云崖", "萨维奈槲寄生", 8.0, 13.0, "巨龙首营地")) return;
                        if (Apply("鲜血滨", "多刺菠萝", 30.0, 26.0, "太阳海岸")) return;
                        if (Apply("银泪湖北岸", "火之晶簇", 32.0, 11.0, "丧灵钟")) return;
                        if (Apply("桤木泉", "绯红树汁", 18.0, 26.0, "秋瓜浮村")) return;
                        if (Apply("盛夏滩", "黄杏", 19.0, 16.0, "盛夏滩 Farms")) return;
                        if (Apply("石绿湖", "鲜血橙", 28.0, 25.0, "Camp 石绿湖")) return;
                        if (Apply("高径", "黑衣香木", 18.0, 23.0, "恬静路营地")) return;
                        if (Apply("翠泪择伐区", "高级黑衣香木", 30.0, 19.0, "弯枝牧场")) return;
                        if (Apply("和风流地", "黑檀原木", 23.2, 26.3, "盛夏农庄")) return;
                        if (Apply("弯枝", "丝柏原木", 25.0, 29.0, "弯枝 Meadows")) return;
                        break;
                    case 50 + GatheringType.Mining:
                        if (Apply("巨龙首", "玄铁矿", 27.6, 19.8, "Camp 巨龙首")) return;
                        if (Apply("火墙", "金矿", 28.0, 22.0, "枯骨营地")) return;
                        if (Apply("劳班缓冲地", "钨铁矿", 16.0, 19.0, "青磷精炼所")) return;
                        if (Apply("黑尘", "自然金", 24.0, 16.0, "黑尘 Station")) return;
                        if (Apply("莫拉比湾", "红宝石原石", 23.0, 21.0, "莫拉比造船厂")) return;
                        if (Apply("秽水", "白金矿", 19.0, 8.0, "小阿拉米格")) return;
                        if (Apply("蓝雾", "石蜥蜴的初蛋", 24.0, 26.0, "青磷精炼所")) return;
                        if (Apply("交汇河", "黄铜矿", 30.0, 23.0, "隼巢")) return;
                        break;
                    case 50 + GatheringType.Quarrying:
                        if (Apply("接雨草树林", "拉诺西亚岩盐", 21.0, 32.0, "葡萄酒港")) return;
                        if (Apply("巨龙首", "星性岩", 23.0, 23.0, "Camp 巨龙首")) return;
                        if (Apply("新植林", "金沙", 25.0, 22.0, "枯骨营地")) return;
                        if (Apply("银泪湖北岸", "火之晶簇", 27.0, 10.0, "丧灵钟")) return;
                        if (Apply("和风流地", "3级拉诺西亚土壤", 25.0, 27.0, "盛夏农庄")) return;
                        if (Apply("低径", "3级黑衣森林土壤", 16.0, 31.0, "恬静路营地")) return;
                        if (Apply("金锤台地", "3级萨纳兰土壤", 18.0, 27.0, "地平关")) return;
                        if (Apply("新植林", "强灵性岩", 26.0, 19.0, "枯骨营地")) return;
                        if (Apply("接雨草树林", "浮石", 17.0, 26.0, "葡萄酒港")) return;
                        break;
                }

            if (node.Meta!.NodeType == NodeType.Regular)
                switch (node.Meta.Level + node.Meta.GatheringType) {
                    case 80 + GatheringType.Harvesting:
                        if (Apply("尾之道", "黄新薯", 19.0, 16.0, "鼹灵集市")) return;
                        if (Apply("射手露宿地", "迷雾菠菜", 34.0, 21.0, "法诺村")) return;
                        if (Apply("无垢之证", "泡茧", 27.0, 10.0, "乔布要塞")) return;
                        if (Apply("白油瀑布", "柔嫩茴香", 28.4, 21.1, "滞潮村", true)) return;
                        if (Apply("拿巴示断绝", "紫草", 32.2, 33.4, "上路客店", true)) return;
                        if (Apply("快婿树岛", "苍棉花", 33.7, 13.2, "法诺村", true)) return;
                        break;
                    case 80 + GatheringType.Logging:
                        if (Apply("刺舌滴", "小柠檬", 20.0, 27.0, "工匠村")) return;
                        if (Apply("缪栎的乡愁", "檀香木原木", 24.0, 36.0, "蛇行枝")) return;
                        if (Apply("安登小羊圈", "印茄原木", 36.5, 27.3, "群花馆", true)) return;
                        if (Apply("迦利克村", "银山毛榉原木", 16.0, 10.9, "络尾聚落", true)) return;
                        if (Apply("责罚监狱", "硬质金合欢树皮", 5.2, 26.5, "奥斯塔尔严命城", true)) return;
                        if (Apply("中琥珀丘", "收藏用沙柚木原木", 18.6, 20.5, "络尾聚落")) return;
                        if (Apply("诺弗兰特大陆坡", "收藏用海藻", 37.5, 11.7, "鳍人潮池")) return;
                        if (Apply("石楠瀑布", "收藏用乌仑代硬木原木", 30.6, 6.9, "阿拉加纳")) return;
                        break;
                    case 80 + GatheringType.Mining:
                        if (Apply("水站", "三重石原石", 20.0, 29.0, "上路客店")) return;
                        if (Apply("杂草岛", "透锂长石原石", 28.0, 33.0, "乔布要塞")) return;
                        if (Apply("卡利班深海峡", "黑玛瑙原石", 16.0, 21.0, "马克连萨斯广场")) return;
                        if (Apply("圣法斯里克天庭", "彩虹晶", 30.0, 20.0, "云村")) return;
                        if (Apply("陆人墓标", "重钨矿", 32.0, 7.0, "鳍人潮池")) return;
                        if (Apply("贤岛", "金锡矿", 4.7, 33.9, "奥斯塔尔严命城", true)) return;
                        if (Apply("破裂卵壳", "苦灰石", 7.5, 30.1, "蛇行枝", true)) return;
                        if (Apply("侏儒烟囱", "超硬水", 36.1, 12.2, "图姆拉村", true)) return;
                        if (Apply("鳍人潮池", "收藏用海底岩", 32.7, 20.6, "鳍人潮池")) return;
                        if (Apply("诺弗兰特大陆坡", "收藏用海盐", 25.1, 4.5, "鳍人潮池")) return;
                        if (Apply("无垢干谷", "收藏用基拉巴尼亚明矾", 31.6, 31.4, "对等石")) return;
                        break;
                    case 80 + GatheringType.Quarrying:
                        if (Apply("尊紫洞", "尊紫洞 Shell Chip", 34.4, 31.3, "马克连萨斯广场", true)) return;
                        if (Apply("比朗大矿山", "灰明矾", 20.1, 8.6, "络尾聚落", true)) return;
                        if (Apply("孚布特堡", "白夜结晶", 35.6, 8.7, "云村", true)) return;
                        break;

                    case 75 + GatheringType.Logging:
                        if (Apply("凿岩门", "收藏用白橡木原木", 28.0, 32.8, "滞潮村")) return;
                        if (Apply("羊毛道", "收藏用仙子苹果", 4.1, 23.1, "群花馆")) return;
                        break;
                    case 75 + GatheringType.Harvesting:
                        if (Apply("群树馆", "蚕豆", 24.0, 36.0, "群花馆")) return;
                        if (Apply("射孔", "欧薄荷", 26.0, 20.0, "乔布要塞")) return;
                        break;
                    case 75 + GatheringType.Mining:
                        if (Apply("六子浅滩", "硬水铝石原石", 26.0, 13.0, "云村")) return;
                        if (Apply("书著者树洞", "青金石原石", 25.0, 34.0, "蛇行枝")) return;
                        if (Apply("束带路", "收藏用钛铜矿", 31.3, 24.2, "乔布要塞")) return;
                        if (Apply("书著者树洞", "收藏用青金石原石", 16.5, 18.2, "蛇行枝")) return;
                        break;

                    case 70 + GatheringType.Harvesting:
                        if (Apply("红锈岩山", "祝圣罗勒草", 23.0, 16.0, "阿拉加纳")) return;
                        if (Apply("七彩溪谷", "莲藕", 28.0, 7.0, "烈士庵")) return;
                        if (Apply("阿巴拉提亚龙头", "长颈骆姜", 8.0, 8.0, "天营门")) return;
                        if (Apply("昂萨哈凯尔", "苎麻", 20.0, 8.0, "晨曦王座")) return;
                        if (Apply("约恩山", "真麻", 24.0, 36.0, "阿拉基利")) return;
                        if (Apply("无二江流域", "延夏棉", 28.0, 35.0, "茨菰村水塘")) return;
                        break;
                    case 70 + GatheringType.Logging:
                        if (Apply("昂萨哈凯尔", "奥萨德梅", 27.0, 17.0, "晨曦王座")) return;
                        if (Apply("高岸", "榧木原木", 11.0, 13.0, "天营门")) return;
                        if (Apply("尖枪瀑布", "黑柳原木", 15.0, 21.0, "帝国东方堡")) return;
                        if (Apply("红锈岩山", "乌仑代硬木原木", 32.0, 10.0, "阿拉加纳")) return;
                        if (Apply("温蒙特造船厂", "白橡木枝", 12.0, 29.0, "工匠村")) return;
                        if (Apply("多玛", "收藏用松脂", 18.7, 14.4, "烈士庵")) return;
                        break;
                    case 70 + GatheringType.Mining:
                        if (Apply("红锈岩山", "蔷薇辉石原石", 26.0, 12.0, "阿拉加纳")) return;
                        if (Apply("七彩溪谷", "琅玕原石", 29.0, 9.0, "烈士庵")) return;
                        if (Apply("挡风巨岩", "石青原石", 5.0, 29.0, "朵洛衣楼")) return;
                        if (Apply("白鬼岩山", "铬铁矿", 16.0, 33.9, "阿拉基利")) return;
                        if (Apply("多玛", "钯金矿", 20.5, 10.4, "烈士庵")) return;
                        if (Apply("奥萨德东岸", "夜铁矿", 11.0, 23.0, "自凝岛近海")) return;
                        if (Apply("月神沙漠北端", "清银矿", 23.0, 36.0, "重逢集市")) return;
                        if (Apply("无垢干谷", "岩铁", 31.0, 27.0, "对等石")) return;
                        if (Apply("盐湖", "常辉矿", 22.0, 13.0, "天营门")) return;
                        if (Apply("总督田地", "乌钢石原石", 33.0, 23.0, "滞潮村")) return;
                        if (Apply("刃海", "收藏用石青原石", 36.0, 26.4, "重逢集市")) return;
                        break;
                    case 70 + GatheringType.Quarrying:
                        if (Apply("盐湖", "阿拉米格盐", 21.0, 29.0, "阿拉米格人居住区")) return;
                        break;

                    case 65 + GatheringType.Logging:
                        if (Apply("无二江流域", "竹笋", 28.0, 25.0, "茨菰村水塘")) return;
                        if (Apply("渔村沿岸", "收藏用落叶松原木", 6.0, 15.8, "自凝岛近海")) return;
                        if (Apply("冲之岩近海", "收藏用香菇", 33.1, 9.2, "自凝岛近海", true)) return; // Aetheryte
                        break;
                    case 65 + GatheringType.Mining:
                        if (Apply("奥萨德东岸", "星尖石原石", 15.0, 4.5, "自凝岛近海")) return;
                        if (Apply("幻河", "收藏用锂辉石原石", 29.6, 12.9, "神拳痕", true)) return; // Aetheryte
                        if (Apply("尖枪瀑布", "收藏用基拉巴尼亚矿泉水", 18.1, 22.8, "对等石")) return;
                        if (Apply("蟹茹滨", "收藏用星尖石原石", 21.2, 34.4, "黄金港城区", true)) return; // Aetheryte
                        break;

                    case 60 + GatheringType.Harvesting:
                        if (Apply("德尔塔管区", "星棉", 9.0, 31.0, "螺旋港")) return;
                        if (Apply("陆行鸟之森", "显贵鼠尾草", 33.0, 30.0, "尾羽集落")) return;
                        if (Apply("双子池", "卡贝基野菜", 8.0, 9.0, "尾羽集落")) return;
                        if (Apply("双子池", "香草豆", 23.0, 21.0, "隼巢")) return;
                        if (Apply("绿茵岛", "星极花", 17.0, 36.0, "天极白垩宫")) return;
                        if (Apply("萨雷安精制区", "鲜红罗兰莓", 39.0, 26.0, "不洁三塔")) return;
                        break;
                    case 60 + GatheringType.Logging:
                        if (Apply("萨雷安治学区", "柚木原木", 6.0, 28.0, "田园郡")) return;
                        if (Apply("绿茵岛", "棕菇", 12.0, 37.0, "天极白垩宫")) return;
                        if (Apply("温杜属本杜集落", "伊修加德柠檬", 35.0, 23.0, "云顶营地")) return;
                        if (Apply("萨雷安睿哲区", "云海香蕉", 19.0, 36.0, "田园郡")) return;
                        if (Apply("试炼群岛", "甜扁桃", 24.0, 6.0, "尊杜集落")) return;
                        if (Apply("贝塔管区", "金合欢树皮", 22.0, 10.0, "螺旋港")) return;
                        if (Apply("竖骨岛", "樟木古树", 11.0, 10.0, "天极白垩宫")) return;
                        if (Apply("东境混交林", "山毛榉树枝", 11.0, 18.0, "帝国东方堡")) return;
                        if (Apply("阿瓦隆尼亚古陆", "收藏用毛栗子", 16.2, 35.5, "不洁三塔")) return;
                        break;
                    case 60 + GatheringType.Mining:
                        if (Apply("双子池", "钨华", 10.0, 9.0, "尾羽集落")) return;
                        if (Apply("阿尔法管区", "辉金矿", 5.0, 17.0, "螺旋港")) return;
                        if (Apply("萨雷安睿哲区", "赤铁矿", 34.0, 30.0, "不洁三塔")) return;
                        if (Apply("贝塔管区", "精金矿", 24.0, 6.0, "螺旋港")) return;
                        if (Apply("绿茵岛", "皇金矿", 11.0, 38.0, "天极白垩宫")) return;
                        if (Apply("生态园", "红明矾", 35.0, 16.0, "螺旋港")) return;
                        if (Apply("试炼群岛", "菱锌矿", 38.0, 15.0, "尊杜集落")) return;
                        if (Apply("条纹丘", "锂辉石原石", 25.0, 8.0, "帝国东方堡")) return;
                        if (Apply("蓝天窗", "收藏用阿巴拉提亚天然水", 20.6, 11.6, "尊杜集落")) return;
                        break;
                    case 60 + GatheringType.Quarrying:
                        if (Apply("萨雷安睿哲区", "沸石", 13.0, 31.0, "田园郡")) return;
                        if (Apply("试炼群岛", "阿巴拉提亚岩盐", 7.0, 7.0, "尊杜集落")) return;
                        if (Apply("交汇河", "星砂砾", 37.0, 16.0, "隼巢")) return;
                        if (Apply("沃仙曦染", "金云母", 35.0, 39.0, "云顶营地")) return;
                        if (Apply("萨雷安治学区", "绀碧石英", 11.0, 16.0, "田园郡")) return;
                        break;
                    case 55 + GatheringType.Harvesting:
                        if (Apply("交汇河", "小包心菜", 31.0, 20.0, "隼巢")) return;
                        break;
                    case 55 + GatheringType.Logging:
                        if (Apply("竖骨岛", "牛肝菌", 24.0, 6.0, "天极白垩宫")) return;
                        if (Apply("荒烟野地", "收藏用暗栗木树液", 29.2, 30.1, "尾羽集落")) return;
                        break;
                    case 55 + GatheringType.Mining:
                        if (Apply("荒烟野地", "黄铁矿", 26.0, 17.0, "尾羽集落")) return;
                        if (Apply("荒烟野地", "收藏用黄铁矿", 30.7, 32.2, "尾羽集落")) return;
                        break;
                    case 55 + GatheringType.Quarrying:
                        if (Apply("招恶荒岛", "翠绿石英", 33.0, 22.0, "莫古力之家")) return;
                        break;

                    case 50 + GatheringType.Harvesting:
                        if (Apply("九藤", "蚕茧", 22.0, 26.0, "霍桑山寨")) return;
                        if (Apply("荆棘森", "延龄花", 17.0, 19.0, "霍桑山寨")) return;
                        if (Apply("蜜场", "延龄草的球根", 13.0, 23.0, "霍桑山寨")) return;
                        if (Apply("鲜血滨", "蜜柠檬", 26.0, 32.0, "太阳海岸")) return;
                        if (Apply("接雨草树林", "泽梅尔番茄", 18.0, 26.0, "葡萄酒港")) return;
                        if (Apply("枯骨", "黑松露", 12.0, 16.0, "Camp 枯骨")) return;
                        if (Apply("酸模避风港", "提诺尔卡茶叶", 16.0, 20.0, "弯枝牧场")) return;
                        if (Apply("四分石地", "拉诺西亚韭菜", 34.0, 28.0, "雨燕塔殖民地")) return;
                        if (Apply("雪松原", "谢尔达莱嫩菠菜", 32.0, 11.0, "莫拉比造船厂")) return;
                        if (Apply("九藤", "迷迭香", 22.0, 30.0, "霍桑山寨")) return;
                        if (Apply("陆行鸟之森", "旧世界无花果", 26.0, 12.0, "尾羽集落")) return;
                        break;
                    case 50 + GatheringType.Logging:
                        if (Apply("神意之地", "云杉原木", 27.0, 12.0, "巨龙首营地")) return;
                        if (Apply("巨龙首", "吸血枝", 27.0, 24.0, "Camp 巨龙首")) return;
                        if (Apply("白云崖", "萨维奈槲寄生", 8.0, 13.0, "巨龙首营地")) return;
                        if (Apply("鲜血滨", "多刺菠萝", 30.0, 26.0, "太阳海岸")) return;
                        if (Apply("银泪湖北岸", "火之晶簇", 32.0, 11.0, "丧灵钟")) return;
                        if (Apply("桤木泉", "绯红树汁", 18.0, 26.0, "秋瓜浮村")) return;
                        if (Apply("盛夏滩", "黄杏", 19.0, 16.0, "盛夏滩 Farms")) return;
                        if (Apply("石绿湖", "鲜血橙", 28.0, 25.0, "Camp 石绿湖")) return;
                        if (Apply("高径", "黑衣香木", 18.0, 23.0, "恬静路营地")) return;
                        if (Apply("翠泪择伐区", "高级黑衣香木", 30.0, 19.0, "弯枝牧场")) return;
                        if (Apply("和风流地", "黑檀原木", 23.2, 26.3, "盛夏农庄")) return;
                        if (Apply("弯枝", "丝柏原木", 25.0, 29.0, "弯枝 Meadows")) return;
                        break;
                    case 50 + GatheringType.Mining:
                        if (Apply("巨龙首", "玄铁矿", 27.6, 19.8, "Camp 巨龙首")) return;
                        if (Apply("火墙", "金矿", 28.0, 22.0, "枯骨营地")) return;
                        if (Apply("劳班缓冲地", "钨铁矿", 16.0, 19.0, "青磷精炼所")) return;
                        if (Apply("黑尘", "自然金", 24.0, 16.0, "黑尘 Station")) return;
                        if (Apply("莫拉比湾", "红宝石原石", 23.0, 21.0, "莫拉比造船厂")) return;
                        if (Apply("秽水", "白金矿", 19.0, 8.0, "小阿拉米格")) return;
                        if (Apply("蓝雾", "石蜥蜴的初蛋", 24.0, 26.0, "青磷精炼所")) return;
                        if (Apply("交汇河", "黄铜矿", 30.0, 23.0, "隼巢")) return;
                        break;
                    case 50 + GatheringType.Quarrying:
                        if (Apply("接雨草树林", "拉诺西亚岩盐", 21.0, 32.0, "葡萄酒港")) return;
                        if (Apply("巨龙首", "星性岩", 23.0, 23.0, "Camp 巨龙首")) return;
                        if (Apply("新植林", "金沙", 25.0, 22.0, "枯骨营地")) return;
                        if (Apply("银泪湖北岸", "火之晶簇", 27.0, 10.0, "丧灵钟")) return;
                        if (Apply("和风流地", "3级拉诺西亚土壤", 25.0, 27.0, "盛夏农庄")) return;
                        if (Apply("低径", "3级黑衣森林土壤", 16.0, 31.0, "恬静路营地")) return;
                        if (Apply("金锤台地", "3级萨纳兰土壤", 18.0, 27.0, "地平关")) return;
                        if (Apply("新植林", "强灵性岩", 26.0, 19.0, "枯骨营地")) return;
                        if (Apply("接雨草树林", "浮石", 17.0, 26.0, "葡萄酒港")) return;
                        break;
                }


            if (node.Meta.NodeType == NodeType.Regular) {
                switch (node.Meta.Level + node.Meta.GatheringType) {
                    case 80 + GatheringType.Harvesting:
                        if (Apply("灰烬池", "改良用的琥珀", 10.6, 18.5, "尾羽集落")) return;
                        if (Apply("碎石山地", "薄暗灵砂", 12.0, 14.0, "图姆拉村")) return;
                        if (Apply("白鲸头冠", "改良用的乳胶", 29.2, 23.4, "尊杜集落")) return;
                        if (Apply("野园", "甜香荠", 24.0, 28.0, "蛇行枝")) return;
                        if (Apply("杂草岛", "虎百合", 23.0, 34.0, "乔布要塞")) return;
                        if (Apply("琥珀原", "收藏用黑夜胡椒", 26.7, 27.5, "上路客店")) return;
                        break;
                    case 80 + GatheringType.Logging:
                        if (Apply("四臂广场", "改良用的琥珀", 25.0, 28.2, "莫古力之家")) return;
                        if (Apply("巨人的兔窝", "香杏", 8.0, 9.0, "奥斯塔尔严命城")) return;
                        if (Apply("水站", "琥珀丁香", 15.0, 30.0, "络尾聚落")) return;
                        if (Apply("宽慰河", "改良用的乳胶", 29.7, 18.3, "尾羽集落")) return;
                        if (Apply("七彩溪谷", "改良用暗栗木原木", 28.3, 9.5, "烈士庵")) return;
                        if (Apply("破坏神像", "改良用暗栗木原木", 20.3, 18.0, "天营门")) return;
                        if (Apply("冠毛大树", "改良用古代原木", 7.0, 34.2, "螺旋港")) return;
                        if (Apply("超星际通信塔", "改良用古代原木", 7.1, 15.9, "螺旋港")) return;
                        if (Apply("对偶处刑台", "Oddly Delicate Feather", 11.6, 26.8, "螺旋港")) return;
                        break;
                    case 80 + GatheringType.Mining:
                        if (Apply("彻悟岩窟", "改良用的暗物质", 17.1, 10.8, "不洁三塔")) return;
                        if (Apply("瓦纳温杜集落", "改良用的黑曜石", 26.5, 23.1, "尊杜集落")) return;
                        if (Apply("红沿", "改良用的黑曜石", 14.8, 30.6, "隼巢")) return;
                        if (Apply("光耀教会", "地下天然水", 35.0, 16.0, "乔布要塞")) return;
                        if (Apply("野园", "强发泡水", 23.0, 28.0, "蛇行枝")) return;
                        if (Apply("大龙瀑水底", "改良用黑碧玺", 12.5, 9.7, "烈士庵")) return;
                        if (Apply("贫民的金矿", "改良用黑碧玺", 4.0, 26.7, "天营门")) return;
                        if (Apply("德尔塔水务局", "改良用古代矿石", 11.0, 35.2, "螺旋港")) return;
                        if (Apply("综合冷却局", "改良用古代矿石", 14.0, 9.8, "螺旋港")) return;
                        if (Apply("伽马管区", "Oddly Delicate Adamantite Ore", 36.2, 27.8, "螺旋港")) return;
                        break;
                    case 80 + GatheringType.Quarrying:
                        if (Apply("友好村", "薄暗灵砂", 22.0, 16.0, "图姆拉村")) return;
                        if (Apply("比朗大矿山", "钛铜沙", 15.0, 12.0, "络尾聚落")) return;
                        if (Apply("小刀石柱群", "改良用的暗物质", 30.9, 28.2, "不洁三塔")) return;
                        if (Apply("希秋亚湿原", "收藏用魔银沙", 16.0, 28.8, "蛇行枝")) return;
                        break;
                    case 75 + GatheringType.Harvesting:
                        if (Apply("湍流三角地", "采集工具的素材", 13.9, 32.7, "田园郡")) return;
                        if (Apply("观海塔", "山地小麦", 16.0, 36.0, "工匠村")) return;
                        if (Apply("瞑目石兵", "皇家葡萄", 16.0, 29.0, "蛇行枝")) return;
                        if (Apply("迷途羊倌之森", "野生生物的遗留物", 16.0, 23.0, "奥斯塔尔严命城")) return;
                        if (Apply("候鸟云巢", "工匠工具部件的素材", 35.9, 36.0, "云顶营地")) return;
                        if (Apply("羊毛道", "巨大生物的进食痕迹", 11.0, 24.0, "群花馆")) return;
                        if (Apply("弓束官邸", "印刷必需品的素材", 17.2, 30.8, "茨菰村水塘")) return;
                        if (Apply("拉克汕城", "收藏用白麻", 23.4, 13.1, "乔布要塞")) return;
                        break;
                    case 75 + GatheringType.Logging:
                        if (Apply("藏泪丘", "招待菜肴的素材", 35.0, 8.5, "阿拉加纳")) return;
                        if (Apply("光明崖", "白橡木原木", 27.0, 22.0, "工匠村")) return;
                        if (Apply("千山", "油橄榄", 36.0, 24.0, "乔布要塞")) return;
                        if (Apply("达穆伦巡礼教会遗迹", "仙子苹果", 30.0, 5.0, "云村")) return;
                        if (Apply("悲叹飞泉", "缝纫工具的素材", 34.3, 11.6, "尾羽集落")) return;
                        if (Apply("结誓洞窟", "大蜜蜂的巢", 12.0, 20.0, "蛇行枝")) return;
                        break;
                    case 75 + GatheringType.Mining:
                        if (Apply("笃学者庄园", "高地天然水", 8.0, 20.0, "群花馆")) return;
                        if (Apply("女王花园", "印刷必需品的素材", 29.7, 16.2, "阿拉米格人居住区")) return;
                        if (Apply("破碎脊骨", "工匠工具部件的素材", 28.9, 7.5, "尊杜集落")) return;
                        if (Apply("杂草岛", "野生生物的遗留物", 26.0, 34.0, "乔布要塞")) return;
                        if (Apply("西方水泉", "缝纫工具的素材", 8.0, 7.8, "天极白垩宫")) return;
                        if (Apply("迷途羊倌之森", "收藏用灵青岩", 27.4, 21.6, "乔布要塞")) return;
                        break;
                    case 75 + GatheringType.Quarrying:
                        if (Apply("洛查特尔大阶梯", "魔银沙", 17.0, 19.0, "蛇行枝")) return;
                        if (Apply("缓慢道", "硬金沙", 21.0, 30.0, "工匠村")) return;
                        if (Apply("圣菲内雅连队露营地", "采集工具的素材", 13.4, 23.9, "隼巢")) return;
                        if (Apply("The Kobayashi Maru", "招待菜肴的素材", 39.4, 4.0, "自凝岛")) return;
                        break;
                    case 70 + GatheringType.Harvesting:
                        if (Apply("多玛", "白萝卜", 22.0, 10.0, "烈士庵")) return;
                        if (Apply("约恩山", "基拉巴尼亚胡萝卜", 26.0, 27.0, "阿拉基利")) return;
                        if (Apply("月神沙漠北端", "日出甘蓝", 14.0, 26.0, "晨曦王座")) return;
                        if (Apply("螺旋海峡", "红玉海带", 11.0, 13.0, "自凝岛近海")) return;
                        if (Apply("鼻之塔", "野生生物的痕迹", 31.0, 15.0, "鼹灵集市")) return;
                        if (Apply("优雅馆", "美丽的辉石", 31.3, 34.2, "群花馆")) return;
                        break;
                    case 70 + GatheringType.Logging:
                        if (Apply("阿巴拉提亚龙头", "榉木原木", 26.0, 9.0, "天营门")) return;
                        if (Apply("昏暗林", "柿子叶", 10.0, 30.0, "帝国东方堡")) return;
                        if (Apply("观石塔", "炭块", 17.2, 24.4, "工匠村")) return;
                        break;
                    case 70 + GatheringType.Mining:
                        if (Apply("高岸", "辉钼矿", 10.0, 18.0, "天营门")) return;
                        if (Apply("优雅馆", "美丽的辉石", 33.3, 31.5, "群花馆")) return;
                        if (Apply("祖灵笑", "翠银矿", 36.0, 19.0, "茨菰村水塘")) return;
                        break;
                    case 70 + GatheringType.Quarrying:
                        if (Apply("昂萨哈凯尔", "翠银沙", 29.0, 15.0, "晨曦王座")) return;
                        if (Apply("落影崖", "炭块", 11.8, 26.3, "工匠村")) return;
                        if (Apply("上路客店", "野生生物的痕迹", 30.0, 25.0, "上路客店")) return;
                        break;
                    case 65 + GatheringType.Harvesting:
                        if (Apply("昏暗林", "神圣罗勒草", 11.0, 26.0, "帝国东方堡")) return;
                        if (Apply("奥萨德东岸", "大豆", 7.0, 8.0, "自凝岛近海")) return;
                        if (Apply("戈尔加涅牧草地", "保温材料", 32.0, 15.0, "隼巢")) return;
                        if (Apply("竖骨岛", "云海之羽", 8.0, 15.0, "天极白垩宫")) return;
                        if (Apply("螺旋海峡", "红玉藻", 26.0, 19.0, "碧玉水附近")) return;
                        if (Apply("林梢", "山芋", 13.0, 11.0, "阿拉加纳")) return;
                        if (Apply("幻河", "收藏用赤麻", 29.0, 11.0, "神拳痕")) return;
                        break;
                    case 65 + GatheringType.Logging:
                        if (Apply("东境混交林", "山毛榉原木", 10.0, 16.0, "帝国东方堡")) return;
                        if (Apply("自凝岛近海", "落叶松原木", 20.0, 9.0, "自凝岛近海")) return;
                        if (Apply("苍鹭瀑布", "松脂", 36.0, 15.0, "茨菰村水塘")) return;
                        break;
                    case 65 + GatheringType.Mining:
                        if (Apply("狱之盖近海", "魔铜矿", 25.0, 35.0, "黄金港城区")) return;
                        if (Apply("大脚雪人居所", "保温材料", 22.0, 35.0, "隼巢")) return;
                        if (Apply("玄水连山", "石间清水", 33.0, 22.0, "茨菰村水塘")) return;
                        if (Apply("条纹丘", "基拉巴尼亚矿泉水", 23.0, 13.0, "帝国东方堡")) return;
                        if (Apply("尖枪瀑布", "收藏用基拉巴尼亚矿泉水", 18.1, 22.8, "对等石")) return;
                        break;
                    case 65 + GatheringType.Quarrying:
                        if (Apply("人王遗迹", "云海之羽", 34.0, 25.0, "莫古力之家")) return;
                        if (Apply("螺旋海峡", "硅藻土", 14.0, 16.0, "自凝岛近海")) return;
                        if (Apply("红锈岩山", "硅岩", 21.0, 13.0, "阿拉加纳")) return;
                        break;
                    case 60 + GatheringType.Harvesting:
                        if (Apply("阿瓦隆尼亚古陆", "紫锥花", 10.0, 33.0, "不洁三塔")) return;
                        if (Apply("东境混交林", "赤麻", 16.0, 7.0, "帝国东方堡")) return;
                        if (Apply("四臂广场", "蒲公英", 20.0, 31.0, "莫古力之家")) return;
                        if (Apply("萨雷安治学区", "菊蒿", 14.0, 19.0, "田园郡")) return;
                        if (Apply("蓝天窗", "美拉西迪亚芝麻", 23.0, 10.0, "尊杜集落")) return;
                        if (Apply("龟甲岛近海", "紫州小判金币", 37.0, 18.0, "自凝岛近海")) return;
                        if (Apply("林梢", "基拉巴尼亚色素", 13.0, 7.0, "阿拉加纳")) return;
                        if (Apply("双子池", "芸香", 12.0, 14.0, "尾羽集落")) return;
                        break;
                    case 60 + GatheringType.Logging:
                        if (Apply("招恶荒岛", "樟木原木", 31.0, 30.0, "莫古力之家")) return;
                        if (Apply("沃仙曦染", "桦树枝", 25.0, 34.0, "云顶营地")) return;
                        break;
                    case 60 + GatheringType.Mining:
                        if (Apply("阿瓦隆尼亚古陆", "蛋白石原石", 20.0, 26.0, "尾羽集落")) return;
                        if (Apply("东境混交林", "基拉巴尼亚明矾", 15.0, 12.0, "帝国东方堡")) return;
                        if (Apply("红沿", "黄水晶原石", 23.0, 30.0, "隼巢")) return;
                        if (Apply("睡石矿场", "基拉巴尼亚色素", 35.0, 10.0, "阿拉加纳")) return;
                        if (Apply("萨雷安精制区", "贵橄榄石原石", 27.0, 24.0, "田园郡")) return;
                        if (Apply("苍玉海沟", "紫州小判金币", 17.0, 33.0, "碧玉水附近")) return;
                        if (Apply("沃仙曦染", "阿巴拉提亚天然水", 33.0, 31.0, "云顶营地")) return;
                        break;
                    case 60 + GatheringType.Quarrying:
                        if (Apply("人王遗迹", "硬银沙", 29.0, 18.0, "莫古力之家")) return;
                        break;
                    case 55 + GatheringType.Harvesting:
                        if (Apply("陆行鸟之森", "高地小麦", 36.0, 20.0, "尾羽集落")) return;
                        if (Apply("人王遗迹", "熔岩甜菜", 20.0, 21.0, "天极白垩宫")) return;
                        if (Apply("双子池", "虹棉", 17.0, 16.0, "隼巢")) return;
                        if (Apply("库尔札斯河", "收藏用虹棉", 24.7, 14.3, "隼巢")) return;
                        break;
                    case 55 + GatheringType.Logging:
                        if (Apply("荒烟野地", "暗栗木原木", 25.0, 25.0, "尾羽集落")) return;
                        break;
                    case 55 + GatheringType.Mining:
                        if (Apply("陆行鸟之森", "星红石原石", 30.0, 16.0, "尾羽集落")) return;
                        if (Apply("戈尔加涅牧草地", "水淙石原石", 31.0, 12.0, "隼巢")) return;
                        if (Apply("荒烟野地", "玛瑙原石", 27.0, 31.0, "尾羽集落")) return;
                        break;
                    case 55 + GatheringType.Quarrying:
                        if (Apply("双子池", "灵银沙", 16.0, 12.0, "尾羽集落")) return;
                        if (Apply("铁杉村", "收藏用灵银沙", 35.9, 23.4, "隼巢")) return;
                        break;
                    case 50 + GatheringType.Harvesting:
                        if (Apply("撒沟厉沙漠", "萨纳兰茶叶", 13.0, 31.0, "遗忘绿洲")) return;
                        break;
                    case 50 + GatheringType.Logging:
                        if (Apply("接雨草树林", "水之碎晶", 17.0, 32.0, "葡萄酒港")) return;
                        if (Apply("交汇河", "雪松原木", 30.0, 32.0, "隼巢")) return;
                        if (Apply("盛夏滩", "火之碎晶", 19.0, 21.0, "盛夏滩 Farms")) return;
                        if (Apply("荆棘森", "紫檀原木", 16.0, 23.0, "霍桑山寨")) return;
                        if (Apply("执掌峡谷", "雷之碎晶", 29.0, 19.0, "黑尘驿站")) return;
                        break;
                    case 50 + GatheringType.Mining:
                        if (Apply("蓝雾", "钴铁矿", 22.0, 24.0, "青磷精炼所")) return;
                        if (Apply("交汇河", "巨龙黑曜石", 28.0, 27.0, "隼巢")) return;
                        break;
                    case 50 + GatheringType.Quarrying:
                        if (Apply("四分石地", "冰之碎晶", 34.0, 28.0, "雨燕塔殖民地")) return;
                        if (Apply("荆棘森", "风之碎晶", 18.0, 24.0, "霍桑山寨")) return;
                        if (Apply("神握角", "土之碎晶", 22.0, 34.0, "莫拉比造船厂")) return;
                        if (Apply("白银集市", "水之碎晶", 18.0, 28.0, "地平关")) return;
                        break;
                    case 45 + GatheringType.Harvesting:
                        if (Apply("石绿湖", "撒沟厉鼠尾草", 35.0, 24.0, "石绿湖营地")) return;
                        break;
                    case 45 + GatheringType.Logging:
                        if (Apply("白云崖", "晶亮苹果", 23.0, 17.0, "巨龙首营地")) return;
                        break;
                    case 45 + GatheringType.Mining:
                        if (Apply("石绿湖", "绿松石原石", 30.0, 25.0, "石绿湖营地")) return;
                        if (Apply("枯骨", "琥珀原石", 12.0, 19.0, "枯骨营地")) return;
                        break;
                    case 45 + GatheringType.Quarrying:
                        if (Apply("石绿湖", "绿金沙", 28.0, 22.0, "石绿湖营地")) return;
                        break;
                    case 40 + GatheringType.Harvesting:
                        if (Apply("低径", "百里香", 21.0, 29.0, "恬静路营地")) return;
                        if (Apply("接雨草树林", "艾蒿", 21.0, 29.0, "葡萄酒港")) return;
                        break;
                    case 40 + GatheringType.Logging:
                        if (Apply("接雨草树林", "铁帽橡果", 19.0, 25.0, "葡萄酒港")) return;
                        break;
                    case 40 + GatheringType.Mining:
                        if (Apply("巨龙首", "锆石原石", 24.0, 19.0, "巨龙首营地")) return;
                        if (Apply("兀尔德恩惠地", "尖晶石原石", 28.0, 22.0, "石场水车")) return;
                        break;
                    case 40 + GatheringType.Quarrying:
                        if (Apply("蓝雾", "榴弹怪的灰", 21.0, 28.0, "青磷精炼所")) return;
                        break;
                    case 35 + GatheringType.Harvesting:
                        if (Apply("鲜血滨", "中原罗勒草", 26.0, 30.0, "太阳海岸")) return;
                        if (Apply("秽水", "芦荟", 20.0, 7.0, "小阿拉米格")) return;
                        if (Apply("低径", "白松露", 17.0, 28.0, "恬静路营地")) return;
                        break;
                    case 35 + GatheringType.Logging:
                        if (Apply("低径", "橡木原木", 16.0, 30.0, "恬静路营地")) return;
                        break;
                    case 35 + GatheringType.Mining:
                        if (Apply("鲜血滨", "海蓝石原石", 28.0, 27.0, "太阳海岸")) return;
                        if (Apply("撒沟厉沙漠", "金绿柱石原石", 25.0, 41.0, "遗忘绿洲")) return;
                        if (Apply("酸模避风港", "橄榄石原石", 14.0, 21.0, "弯枝牧场")) return;
                        break;
                    case 35 + GatheringType.Quarrying:
                        if (Apply("红迷宫", "秘银沙", 17.0, 18.0, "小阿拉米格")) return;
                        break;
                    case 30 + GatheringType.Harvesting:
                        if (Apply("桤木泉", "翡翠豆", 22.0, 25.0, "秋瓜浮村")) return;
                        break;
                    case 30 + GatheringType.Logging:
                        if (Apply("弯枝", "绿色色素", 24.0, 30.0, "弯枝牧场")) return;
                        if (Apply("鲜血滨", "蓝色色素", 28.0, 33.0, "太阳海岸")) return;
                        if (Apply("和平苑", "棕色色素", 27.0, 22.0, "秋瓜浮村")) return;
                        if (Apply("沉默花坛", "鳄梨", 26.0, 19.0, "石场水车")) return;
                        if (Apply("无刺盆地", "紫色色素", 24.0, 31.0, "黑尘驿站")) return;
                        if (Apply("三星里弯陷", "红色色素", 16.0, 13.0, "盛夏农庄")) return;
                        if (Apply("高径", "灰色色素", 16.0, 21.0, "石场水车")) return;
                        break;
                    case 30 + GatheringType.Mining:
                        if (Apply("新植林", "飞龙黑曜石", 26.0, 17.0, "枯骨营地")) return;
                        break;
                    case 30 + GatheringType.Quarrying:
                        if (Apply("秽水", "紫色色素", 18.0, 11.0, "小阿拉米格")) return;
                        if (Apply("雪松原", "棕色色素", 26.0, 15.0, "莫拉比造船厂")) return;
                        if (Apply("九藤", "绿色色素", 20.0, 27.0, "霍桑山寨")) return;
                        if (Apply("丰饶神井", "蓝色色素", 23.0, 23.0, "地平关")) return;
                        if (Apply("橡树原", "硫磺", 12.0, 26.0, "石绿湖营地")) return;
                        if (Apply("四分石地", "灰色色素", 31.0, 28.0, "雨燕塔殖民地")) return;
                        if (Apply("撒沟厉沙漠", "爆弹怪的灰", 22.0, 29.0, "遗忘绿洲")) return;
                        if (Apply("新植林", "红色色素", 23.0, 19.0, "枯骨营地")) return;
                        break;
                    case 25 + GatheringType.Harvesting:
                        if (Apply("枯骨", "草菇", 14.0, 20.0, "枯骨营地")) return;
                        if (Apply("橡树原", "仙子梅", 14.0, 24.0, "石绿湖营地")) return;
                        break;
                    case 25 + GatheringType.Logging:
                        if (Apply("高径", "诺菲卡槲寄生", 23.0, 21.0, "石场水车")) return;
                        break;
                    case 25 + GatheringType.Mining:
                        if (Apply("高径", "银矿", 15.0, 19.0, "石场水车")) return;
                        break;
                    case 25 + GatheringType.Quarrying:
                        if (Apply("橡树原", "火性岩", 12.0, 23.0, "石绿湖营地")) return;
                        if (Apply("高径", "土性岩", 23.0, 21.0, "石场水车")) return;
                        break;
                    case 20 + GatheringType.Harvesting:
                        if (Apply("九藤", "金钱蘑", 18.0, 28.0, "霍桑山寨")) return;
                        if (Apply("四分石地", "红辣椒", 31.0, 28.0, "雨燕塔殖民地")) return;
                        if (Apply("沙门", "新薯", 16.0, 27.0, "枯骨营地")) return;
                        break;
                    case 20 + GatheringType.Logging:
                        if (Apply("黑尘", "仙人掌叶", 21.0, 20.0, "黑尘 Station")) return;
                        if (Apply("雪松原", "太阳柠檬", 34.0, 17.0, "莫拉比造船厂")) return;
                        if (Apply("九藤", "仙女苹果", 15.0, 27.0, "霍桑山寨")) return;
                        if (Apply("骷髅谷", "1级碳化暗物质", 26.0, 23.0, "小麦酒港")) return;
                        break;
                    case 20 + GatheringType.Mining:
                        if (Apply("枯骨", "孔雀石原石", 17.0, 20.0, "枯骨营地")) return;
                        if (Apply("和平苑", "榍石原石", 29.0, 22.0, "秋瓜浮村")) return;
                        if (Apply("骷髅谷", "赛黄晶原石", 29.0, 22.0, "小麦酒港")) return;
                        break;
                    case 20 + GatheringType.Quarrying:
                        if (Apply("骷髅谷", "泥岩", 26.0, 24.0, "小麦酒港")) return;
                        if (Apply("三星里弯陷", "1级碳化暗物质", 15.0, 10.0, "盛夏农庄")) return;
                        break;
                    case 15 + GatheringType.Harvesting:
                        if (Apply("弯枝", "马郁兰", 18.0, 19.0, "弯枝牧场")) return;
                        if (Apply("弯枝", "康乃馨", 22.0, 24.0, "弯枝牧场")) return;
                        if (Apply("地平", "库尔札斯胡萝卜", 23.0, 18.0, "地平关")) return;
                        if (Apply("莫拉比湾", "橄榄", 26.0, 22.0, "莫拉比造船厂")) return;
                        if (Apply("丰饶神井", "大蒜", 23.0, 23.0, "地平关")) return;
                        if (Apply("盛夏滩", "小麦", 22.0, 19.0, "盛夏农庄")) return;
                        if (Apply("执掌峡谷", "高山萝卜", 25.0, 20.0, "黑尘驿站")) return;
                        break;
                    case 15 + GatheringType.Logging:
                        if (Apply("弯枝", "榆木原木", 20.0, 20.0, "弯枝牧场")) return;
                        break;
                    case 15 + GatheringType.Mining:
                        if (Apply("地平", "铁矿", 27.0, 17.0, "地平关")) return;
                        break;
                    case 15 + GatheringType.Quarrying:
                        if (Apply("黑尘", "岩盐", 14.0, 23.0, "黑尘驿站")) return;
                        if (Apply("地平", "朱砂", 24.0, 18.0, "地平关")) return;
                        break;
                    case 10 + GatheringType.Logging:
                        if (Apply("雪松原", "拉诺西亚香橙", 32.0, 16.0, "莫拉比造船厂")) return;
                        if (Apply("翠泪择伐区", "梣木原木", 25.0, 20.0, "弯枝牧场")) return;
                        if (Apply("无刺盆地", "白鸡之羽", 22.0, 26.0, "黑尘驿站")) return;
                        if (Apply("私语巨木", "梣木原木", 27.0, 24.0, "秋瓜浮村")) return;
                        break;
                    case 10 + GatheringType.Mining:
                        if (Apply("黑尘", "锡矿", 20.0, 22.0, "黑尘驿站")) return;
                        if (Apply("雪松原", "日长石原石", 27.0, 18.0, "莫拉比造船厂")) return;
                        if (Apply("金锤台地", "锡矿", 22.0, 28.0, "地平关")) return;
                        if (Apply("私语巨木", "天青石原石", 28.0, 25.0, "秋瓜浮村")) return;
                        break;
                    case 5 + GatheringType.Logging:
                        if (Apply("格尔莫拉遗迹", "土之碎晶", 26.5, 21.9, "秋瓜浮村")) return;
                        if (Apply("翡翠湖滨", "乳胶", 23.0, 18.0, "弯枝牧场")) return;
                        if (Apply("纠缠沼泽林", "雷之碎晶", 14.5, 14.3, "丧灵钟")) return;
                        if (Apply("私语巨木", "乳胶", 28.0, 26.0, "秋瓜浮村")) return;
                        if (Apply("私语巨木", "枫树树汁", 25.0, 27.0, "秋瓜浮村")) return;
                        if (Apply("白云崖", "冰之碎晶", 26.6, 19.7, "巨龙首营地")) return;
                        break;
                    case 5 + GatheringType.Mining:
                        if (Apply("格尔莫拉遗迹", "土之碎晶", 25.3, 22.2, "秋瓜浮村")) return;
                        if (Apply("金锤台地", "铜矿", 26.0, 25.0, "地平关")) return;
                        if (Apply("无刺盆地", "铜矿", 18.8, 25.7, "黑尘驿站")) return;
                        if (Apply("无刺盆地", "骨片", 24.0, 26.0, "黑尘驿站")) return;
                        if (Apply("纠缠沼泽林", "雷之碎晶", 15.2, 14.7, "丧灵钟")) return;
                        if (Apply("白云崖", "冰之碎晶", 28.0, 16.5, "巨龙首营地")) return;
                        break;
                }
                // @formatter:on
            }
        }
    }
}
