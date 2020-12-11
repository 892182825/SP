<%@ Control Language="C#" AutoEventWireup="true" CodeFile="paybottom.ascx.cs" Inherits="UserControl_WebUserControl2" %>
 <div id="J-faq" class="ui-faq">
            <h3>
                
               <%=tean.GetTran("008389","支付遇到问题") %> ：</h3>
            <dl>
                <dt><%=tean.GetTran("008390","选择银行网银付款提示证件号码和昵称错误怎么办") %>？</dt>
                <dd>
                    <%=tean.GetTran("008391","答") %>：<%=tean.GetTran("008392","开通银行网上银行的时候证件号码和昵称不符") %>。</dd>
                <dt><%=tean.GetTran("008393","选择银行网上银行付款提示银行证书错误怎么办") %>？</dt>
                <dd>
                    <%=tean.GetTran("008391","答") %>：<%=tean.GetTran("008394","点击“去网上银行”后提示证书错误") %>。</dd>
                <dt> <%=tean.GetTran("008396","网上银行页面打不开怎么办") %>？</dt>
                <dd>
                   <%=tean.GetTran("008391","答") %>：<%=tean.GetTran("008395","网上银行页面打不开可能有多种原因") %>， <%--<a href="http://help.alipay.com/lab/help_detail.htm?help_id=211659"
                        target="_blank">点此查看解决办法</a>--%>
                </dd>
            </dl>
        <%--    <p class="tip-faq-link">
                <a seed="link-errorHelp-moreHelp" target="_blank" href="http://help.lab.alipay.com/lab/index.htm">
                    更多帮助</a> <a target="_blank" href="https://lab.alipay.com/user/i.htm">进入我的支付宝</a>
            </p>--%>
        </div>