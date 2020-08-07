using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.json_class
{
    public class Brand
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// 尚德机构
        /// </summary>
        public string name { get; set; }
    }

    

    public class Cta
    {
        /// <summary>
        /// 查看详情
        /// </summary>
        public string value { get; set; }
    }

    public class Footer
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }

    public class CreativesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string app_promotion_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Brand brand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Cta cta { get; set; }
        /// <summary>
        /// 工资低？升职慢？可能是学历拖了后腿！现在上班族在职就可读研，大专毕业5年，本科毕业3年即可报名咨询，点击了解！
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Footer footer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string landing_url { get; set; }
        /// <summary>
        /// 在浙江，工作之余读个研究生吧，在职可读，还是名校硕士！
        /// </summary>
        public string title { get; set; }
    }

    public class Ad
    {
        /// <summary>
        /// 
        /// </summary>
        public string ad_verb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Brand brand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> click_tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string close_track { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> close_tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> conversion_tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CreativesItem> creatives { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> debug_tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool display_advertising_tag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string experiment_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> impression_tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool is_evergreen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool is_new_webview { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool is_speeding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool is_webp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool land_prefetch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool native_prefetch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int party_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string revert_close_track { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string template { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string view_track { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> view_tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string za_ad_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string za_ad_info_json { get; set; }
    }

    public class Ad_info
    {
        /// <summary>
        /// 
        /// </summary>
        public Ad ad { get; set; }
        /// <summary>
        /// {"ads":[{"id":616428,"ad_zone_id":236,"template":"web_word","impression_tracks":["http://ggtf.sunland.org.cn/dataservice-servers/dataserviceNew/commonFallback?advertise=zhihu\u0026android_id=__ANDROIDID__\u0026idfa=__IDFA__\u0026imei=__IMEI__\u0026session_id=0f44a9b1-9244-4e89-ac5e-4c72a66e29d8\u0026cb_url=https%3A%2F%2Fsugar.zhihu.com%2Fplutus_adreaper_callback%3Fsi%3D0f44a9b1-9244-4e89-ac5e-4c72a66e29d8%26os%3D3%26zid%3D236%26zaid%3D616428%26zcid%3D638012%26cid%3D638007%26event%3D__EVENTTYPE__%26value%3D__EVENTVALUE__%26ts%3D__TIMESTAMP__%26cts%3D__TS__\u0026ip=115.238.74.182\u0026ua=Go-http-client%2F1.1\u0026os=3\u0026pl_id=106222\u0026cr_id=638007\u0026data_type=show","https://sugar.zhihu.com/plutus_adreaper?idi=11003\u0026ts=1596770532\u0026nt=0\u0026tu=http%3A%2F%2F91ld.com%2F3670%2FCN%2Fmb%2FeqWm3e%2Findex.html%3Fplan_id%3D1008844%26creative_id%3Dzh-MBA_cn-pc_tw_820-B02\u0026pdi=1516006653832020\u0026ed=CjEEeB4wM31-Q2ZTUGkuAUZ8C34Mbm4ldhx_BlJlKR1fJg54WXVsJXwcNl8XMTYNWXYPbFkqPn14FGRTA2hqQAJ4CHoOcWhyaEY7WgdjdABadx8pTH5qZi1Kb1YCYWpSG3gIeQllNjVzFYFi0L3J5Qo9\u0026ut=3fedf7b11f04486da59529d443e5040a\u0026pf=0\u0026ar=0.0002056416432184589\u0026ui=115.238.74.182\u0026au=510"],"view_tracks":["https://sugar.zhihu.com/plutus_adreaper?nt=0\u0026ut=3fedf7b11f04486da59529d443e5040a\u0026ar=0.0002056416432184589\u0026ts=1596770532\u0026ui=115.238.74.182\u0026au=510\u0026idi=11003\u0026pdi=1516006653832020\u0026pf=0\u0026tu=http%3A%2F%2F91ld.com%2F3670%2FCN%2Fmb%2FeqWm3e%2Findex.html%3Fplan_id%3D1008844%26creative_id%3Dzh-MBA_cn-pc_tw_820-B02\u0026ed=CjEEeR4wM31-Q2ZTUGkuAUZ8C34Mbm4ldhx_BlJlKR1fJg54WXVsJXwcNl8XMTYNWXYPbFkqPn14FGRTA2hqQAJ4CHoOcWhyaEY7WgdjdABadx8pTH5qZi1Kb1YCYWpSG3gIeQllNjVzFeru1ZldmdVn"],"click_tracks":["http://ggtf.sunland.org.cn/dataservice-servers/dataserviceNew/commonFallback?advertise=zhihu\u0026android_id=__ANDROIDID__\u0026idfa=__IDFA__\u0026imei=__IMEI__\u0026session_id=0f44a9b1-9244-4e89-ac5e-4c72a66e29d8\u0026cb_url=https%3A%2F%2Fsugar.zhihu.com%2Fplutus_adreaper_callback%3Fsi%3D0f44a9b1-9244-4e89-ac5e-4c72a66e29d8%26os%3D3%26zid%3D236%26zaid%3D616428%26zcid%3D638012%26cid%3D638007%26event%3D__EVENTTYPE__%26value%3D__EVENTVALUE__%26ts%3D__TIMESTAMP__%26cts%3D__TS__\u0026ip=115.238.74.182\u0026ua=Go-http-client%2F1.1\u0026os=3\u0026pl_id=106222\u0026cr_id=638007\u0026data_type=click","https://sugar.zhihu.com/plutus_adreaper?ar=0.0002056416432184589\u0026ui=115.238.74.182\u0026ut=3fedf7b11f04486da59529d443e5040a\u0026ts=1596770532\u0026nt=0\u0026idi=11003\u0026ed=CjEEfh4wM31-Q2ZTUGkuAUZ8C34Mbm4ldhx_BlJlKR1fJg54WXVsJXwcNl8XMTYNWXYPbFkqPn14FGRTA2hqQAJ4CHoOcWhyaEY7WgdjdABadx8pTH5qZi1Kb1YCYWpSG3gIeQllNjVzFZgvC8FssRSl\u0026au=510\u0026pdi=1516006653832020\u0026pf=0\u0026tu=http%3A%2F%2F91ld.com%2F3670%2FCN%2Fmb%2FeqWm3e%2Findex.html%3Fplan_id%3D1008844%26creative_id%3Dzh-MBA_cn-pc_tw_820-B02"],"close_tracks":["https://sugar.zhihu.com/plutus_adreaper?ar=0.0002056416432184589\u0026au=510\u0026idi=11003\u0026ut=3fedf7b11f04486da59529d443e5040a\u0026ed=CjEEfR4wM31-Q2ZTUGkuAUZ8C34Mbm4ldhx_BlJlKR1fJg54WXVsJXwcNl8XMTYNWXYPbFkqPn14FGRTA2hqQAJ4CHoOcWhyaEY7WgdjdABadx8pTH5qZi1Kb1YCYWpSG3gIeQllNjVzFSvaq_K_zKcM\u0026pdi=1516006653832020\u0026ui=115.238.74.182\u0026pf=0\u0026ts=1596770532\u0026nt=0\u0026tu=http%3A%2F%2F91ld.com%2F3670%2FCN%2Fmb%2FeqWm3e%2Findex.html%3Fplan_id%3D1008844%26creative_id%3Dzh-MBA_cn-pc_tw_820-B02"],"debug_tracks":["https://sugar.zhihu.com/plutus_adreaper?ar=0.0002056416432184589\u0026pdi=1516006653832020\u0026ed=CjEEcx4wM31-Q2ZTUGkuAUZ8C34Mbm4ldhx_BlJlKR1fJg54WXVsJXwcNl8XMTYNWXYPbFkqPn14FGRTA2hqQAJ4CHoOcWhyaEY7WgdjdABadx8pTH5qZi1Kb1YCYWpSG3gIeQllNjVzFaMjK7evv3MR\u0026ts=1596770532\u0026au=510\u0026nt=0\u0026tu=http%3A%2F%2F91ld.com%2F3670%2FCN%2Fmb%2FeqWm3e%2Findex.html%3Fplan_id%3D1008844%26creative_id%3Dzh-MBA_cn-pc_tw_820-B02\u0026ut=3fedf7b11f04486da59529d443e5040a\u0026idi=11003\u0026ui=115.238.74.182\u0026pf=0"],"conversion_tracks":["https://sugar.zhihu.com/plutus_adreaper?tu=http%3A%2F%2F91ld.com%2F3670%2FCN%2Fmb%2FeqWm3e%2Findex.html%3Fplan_id%3D1008844%26creative_id%3Dzh-MBA_cn-pc_tw_820-B02\u0026ts=1596770532\u0026idi=11003\u0026pf=0\u0026ui=115.238.74.182\u0026au=510\u0026pdi=1516006653832020\u0026nt=0\u0026ed=CjEEfx4wM31-Q2ZTUGkuAUZ8C34Mbm4ldhx_BlJlKR1fJg54WXVsJXwcNl8XMTYNWXYPbFkqPn14FGRTA2hqQAJ4CHoOcWhyaEY7WgdjdABadx8pTH5qZi1Kb1YCYWpSG3gIeQllNjVzFf91YS2DRWRp\u0026ut=3fedf7b11f04486da59529d443e5040a\u0026ar=0.0002056416432184589"],"extra_conversion_tracks":{"call_back":["https://sugar.zhihu.com/plutus_adreaper_callback?si=0f44a9b1-9244-4e89-ac5e-4c72a66e29d8\u0026os=3\u0026zid=236\u0026zaid=616428\u0026zcid=638012\u0026cid=638007\u0026event=__EVENTTYPE__\u0026value=__EVENTVALUE__\u0026ts=__TIMESTAMP__\u0026cts=__TS__"]},"za_ad_info":"COzPJRDsASIBMV2SWb5OYLz4Jg==","za_ad_info_json":"{\"ad_id\":616428,\"ad_zone_id\":236,\"category\":\"1\",\"timestamp\":1596770600,\"creative_id\":638012}","creatives":[{"id":638012,"asset":{"brand_name":"尚德机构","brand_logo":"https://pic2.zhimg.com/v2-1a571caf874eae474800000609c49299_250x250.jpeg","title":"在浙江，工作之余读个研究生吧，在职可读，还是名校硕士！","desc":"工资低？升职慢？可能是学历拖了后腿！现在上班族在职就可读研，大专毕业5年，本科毕业3年即可报名咨询，点击了解！","landing_url":"http://91ld.com/3670/CN/mb/eqWm3e/index.html?plan_id=1008844\u0026creative_id=zh-MBA_cn-pc_tw_820-B02","img_size":0,"cta":"查看详情","native_asset":{}}}],"expand":{"display_advertising_tag":true,"is_new_webview":true,"is_cdn_speeding":false},"experiment_info":"{}","view_x_tracks":["https://sugar.zhihu.com/plutus_adreaper?au=510\u0026ar=0.0002056416432184589\u0026idi=11003\u0026nt=0\u0026tu=http%3A%2F%2F91ld.com%2F3670%2FCN%2Fmb%2FeqWm3e%2Findex.html%3Fplan_id%3D1008844%26creative_id%3Dzh-MBA_cn-pc_tw_820-B02\u0026pf=0\u0026pdi=1516006653832020\u0026ut=3fedf7b11f04486da59529d443e5040a\u0026ui=115.238.74.182\u0026ed=CjEEewhlKSlzFTRTBTF1UlpoAHgMd3d0Kx1rSlAzeVVGcVp9CiJsdisXawMJdi1KVncKfB4iMyRzE2NRBWJ0FhssBHsIdWhyfAMxDgxmfwhbdAtsWzdncGhGPVoAY30WCTUEewtyfCw7GGKwA2E7buyDyw%3D%3D\u0026ts=1596770532"],"mobile_experiment":{"am_adx_video":"1","am_bid_p":"50","am_bid_ratio":"10000","am_or_ad":"0"},"web_experiment":{"aw_cardtest2":"0","aw_cardtest3":"0","aw_cardtest4":"0","aw_cardtest5":"0","aw_cardtest6":"0"}}]}
        /// </summary>
        public string adjson { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int position { get; set; }
    }



    public class Question
    {
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 我爸不停否认我关于男女平权的观点，并且说我以后在社会上会混不下去，我该怎么办？
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string question_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int created { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int updated_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Relationship relationship { get; set; }
    }

    public class Badge_v2
    {
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<merged_badges> merged_badges { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<detail_badges> detail_badges { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string night_icon { get; set; }
    }

    public class detail_badges
    {
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string detail_type { get; set; }
        /// <summary>
        /// 已认证的个人
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 北海道大学 水产学博士在读
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> sources { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string night_icon { get; set; }
    }


    public class merged_badges
    {
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string detail_type { get; set; }
        /// <summary>
        /// 认证
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 北海道大学 水产学博士在读
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> sources { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string night_icon { get; set; }
    }

    public class Author
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url_token { get; set; }
        /// <summary>
        /// 木槿
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatar_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatar_url_template { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool is_org { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_type { get; set; }
        /// <summary>
        /// 淡。detail_badges
        /// </summary>
        public string headline { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Badge> badge { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Badge_v2 badge_v2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool is_advertiser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool is_privacy { get; set; }
    }

    public class Badge
    {
        public string type { get; set; }
        public string description { get; set; }
        public List<string> topics { get; set; }
    }
    public class Relationship
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> upvoted_followees { get; set; }
    }

    public class DataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string answer_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Question question { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Author author { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool is_collapsed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int created_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int updated_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extras { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool is_copyable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Relationship relationship { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ad_answer { get; set; }
    }

    public class Paging
    {
        /// <summary>
        /// 
        /// </summary>
        public bool is_end { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool is_start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string next { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string previous { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totals { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public Ad_info ad_info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DataItem> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Paging paging { get; set; }
    }

}
