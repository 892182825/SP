/*
 Name:    Highcharts
 Version: 1.0.2 (2009-12-09)
 Author:  Vevstein Web T.H.
 Support: www.highcharts.com/support
 License: www.highcharts.com/license
*/
(function(){function W(a,b){a||(a={});for(var c in b)a[c]=b[c];return a}function wb(a){return Aa=V(Aa,a)}function Ka(a,b){var c=function(){};c.prototype=new a;W(c.prototype,b);return c}function $a(a){for(var b=[],c=a.length-1;c>=0;c--)b.push(a[c]);return b}function xb(a,b){if(typeof a=="string")return a;else if(a.linearGradient){var c=b.createLinearGradient.apply(b,a.linearGradient);z(a.stops,function(d){c.addColorStop(d[0],d[1])});return c}}function X(a,b,c,d,e){a=Fa.createElement(a);b&&W(a,b);e&&
ga(a,{padding:0,border:"none",margin:0});c&&ga(a,c);d&&d.appendChild(a);return a}function ga(a,b){if(Ba){if(b.opacity!==ba)b.filter="alpha(opacity="+b.opacity*100+")";if(b.cssFloat!==ba)b.styleFloat=b.cssFloat}W(a.style,b)}function yb(a,b,c,d){a=a;var e=isNaN(b=ra(b))?2:b;b=c===ba?".":c;d=d===ba?",":d;c=a<0?"-":"";var f=parseInt(a=ra(+a||0).toFixed(e))+"",j=(j=f.length)>3?j%3:0;return c+(j?f.substr(0,j)+d:"")+f.substr(j).replace(/(\d{3})(?=\d)/g,"$1"+d)+(e?b+ra(a-f).toFixed(e).slice(2):"")}function pb(a,
b,c){function d(u){return u.toString().replace(/^([0-9])$/,"0$1")}b=new Date(b*oa);var e=b.getUTCHours(),f=b.getUTCDay(),j=b.getUTCDate(),g=b.getUTCMonth(),l=b.getUTCFullYear();lang=Aa.lang;var m=lang.weekdays,k=lang.months;b={a:m[f].substr(0,3),A:m[f],d:d(j),e:j,b:k[g].substr(0,3),B:k[g],m:d(g+1),y:l.toString().substr(2,2),Y:l,H:d(e),I:d(e%12||12),l:e%12||12,M:d(b.getUTCMinutes()),p:e<12?"AM":"PM",P:e<12?"am":"pm",S:d(b.getUTCSeconds())};for(var o in b)a=a.replace("%"+o,b[o]);return c?a.substr(0,
1).toUpperCase()+a.substr(1):a}function qb(a){for(var b={x:a.offsetLeft,y:a.offsetTop};a.offsetParent;){a=a.offsetParent;b.x+=a.offsetLeft;b.y+=a.offsetTop;if(a!=Fa.body&&a!=Fa.documentElement){b.x-=a.scrollLeft;b.y-=a.scrollTop}}return b}function zb(a){function b(){var p={line:sa,spline:rb,area:Ab,areaspline:Bb,column:jb,bar:Cb,pie:Db,scatter:Eb},t,n;z(a.series,function(C){t=p[C.type||q.defaultSeriesType];n=new t;n.init(r,C);Ga.push(n)})}function c(){var p,t,n,C,L,J=[];z(Ga,function(P){var D=P.options.stacking,
A=D=="percent";if(D)var F=J[P.type]=J[P.type]||[];if(A){C=0;L=99}if(P.needsAxes){p=true;z(P.data,function(w,B){if(t===ba)t=n=w[0];if(C===ba)C=L=/(column|bar)/.test(P.type)?0:w[1];if(w[0]>n)n=w[0];else if(w[0]<t)t=w[0];if(D)F[B]=F[B]?F[B]+w[1]:w[1];B=F?F[B]:w[1];if(!A)if(B>L)L=B;else if(B<C)C=B;if(D)Va[P.type][w[0]]={total:B,cum:B}})}});return{show:p,xMin:t,xMax:n,yMin:C,yMax:L}}function d(){var p=true;for(var t in r.resources)r.resources[t]||(p=false);p&&j()}function e(p){if(p){r.tracker.zoomX&&Wa.setExtremes(p.xMin,
p.xMax);r.tracker.zoomY&&Xa.setExtremes(p.yMin,p.yMax)}else{Wa.reset();Xa.reset()}r.tracker.tooltip.hide();for(var t in Va)z(Va[t],function(n,C){n=n.total;Va[t][C]={total:n,cum:n}});z(r.series,function(n){z(n.areas,function(C){C.parentNode.removeChild(C)});n.translate();n.createArea();n.clear();n.type=="spline"&&n.getSplineData()});if(r.axes.show){Wa.render();Xa.render()}for(i=0;i<Ga.length;i++)Ga[i].render()}function f(){if(!r.titleLayer){var p=r.titleLayer=new da("title-layer",E,null,{zIndex:5});
a.title&&X("h2",{className:"highcharts-title",innerHTML:a.title.text},a.title.style,p.div);a.subtitle&&X("h3",{className:"highcharts-subtitle",innerHTML:a.subtitle.text},a.subtitle.style,p.div)}}function j(){r.axes=c();r.xAxis=Wa=new l(r,a.xAxis,true);r.yAxis=Xa=new l(r,a.yAxis);z(Ga,function(p){p.translate();a.tooltip.enabled&&p.createArea()});r.render=g;r.zoom=e;setTimeout(g,0)}function g(){var p,t=a.labels,n=a.credits;ga(E,W({position:sb,overflow:pa},q.style));if(q.className)E.className+=" "+q.className;
p=2*(q.borderWidth||0)+(q.shadow?8:0);ka.drawRect(p/2,p/2,ca-p,T-p,q.borderColor,q.borderWidth,q.borderRadius,q.backgroundColor,q.shadow);ka.drawRect(Q,Y,ta,na,q.plotBorderColor,q.plotBorderWidth,null,q.plotBackgroundColor,q.plotShadow,ab);if(r.axes.show){Wa.render();Xa.render()}f();t.items&&z(t.items,function(){var C=W({className:"highcharts-label"},this.attributes);Ra.drawHtml(this.html,C,W(t.style,this.style))});for(p=0;p<Ga.length;p++)Ga[p].render();r.legend=new u(r);if(!r.toolbar)r.toolbar=m(r);
if(n.enabled&&!r.credits)r.credits=X("a",{href:n.href,innerHTML:"<span>"+n.text+"</span>"},W(n.style,{zIndex:8}),E)}function l(p,t,n){function C(s,y,M){var H=1,ha=0;if(M){H*=-1;ha=Ha}if(kb){H*=-1;ha-=H*Ha}if(y)return(s-0)/Sa+Z;return H*(s-Z)*Sa+ha}function L(s,y,M){if(M){var H,ha,ua;H=ha=C(s)+bb;s=ua=T-C(s)-bb;if(n){s=Y;ua=T-la}else{H=Q;ha=ca-va}La.drawLine(H,s,ha,ua,y,M)}}function J(s,y,M){var H=(y-s)*Sa;L(s+(y-s)/2,M,H)}function P(s,y,M,H,ha,ua){var qa,S,wa,N=x.labels;if(y=="inside")ha=-ha;y=S=
C(s+Ma)+bb;qa=wa=T-C(s+Ma)-bb;if(n){qa=T-la;wa=qa+ha}else{y=Q;S=y-ha}H&&La.drawLine(y,qa,S,wa,M,H);if(ua&&N.enabled)if((s=cb.call({value:Ca&&Ca[s]?Ca[s]:s}))||s===0)La.addText(s,y+N.x-(Ma&&n?Ma*Sa*(kb?-1:1):0),qa+N.y-(Ma&&!n?Ma*Sa*(kb?1:-1):0),N.style,N.rotation,N.align)}function D(s,y){var M;Ya=y?1:ia.pow(10,xa(ia.log(s)/ia.LN10));M=s/Ya;y||(y=[1,2,2.5,5,10]);for(var H=0;H<y.length;H++){s=y[H];if(M<=(y[H]+(y[H+1]||y[H]))/2)break}s*=Ya;return s}function A(){Da=[];for(var s=1E3/oa,y=6E4/oa,M=36E5/
oa,H=864E5/oa,ha=6048E5/oa,ua=2592E6/oa,qa=31556952E3/oa,S=[["second",s,[1,2,5,10,15,30]],["minute",y,[1,2,5,10,15,30]],["hour",M,[1,2,3,4,6,8,12]],["day",H,[1,2]],["week",ha,[1,2]],["month",ua,[1,2,3,4,6]],["year",qa,null]],wa=S[6],N=wa[1],R=wa[2],ya=0;ya<S.length;ya++){wa=S[ya];N=wa[1];R=wa[2];if(S[ya+1]){var Fb=(N*R[R.length-1]+S[ya+1][1])/2;if(ea<=Fb)break}}if(N==qa&&ea<5*N)R=[1,2,5];S=D(ea/N,R);var Na;R=new Date(Z*oa);R.setUTCMilliseconds(0);if(N>=s)R.setUTCSeconds(N>=y?0:S*xa(R.getUTCSeconds()/
S));if(N>=y)R.setUTCMinutes(N>=M?0:S*xa(R.getUTCMinutes()/S));if(N>=M)R.setUTCHours(N>=H?0:S*xa(R.getUTCHours()/S));if(N>=H)R.setUTCDate(N>=ua?1:S*xa(R.getUTCDate()/S));if(N>=ua){R.setUTCMonth(N>=qa?0:S*xa(R.getUTCMonth()/S));Na=R.getUTCFullYear()}if(N>=qa){Na-=Na%S;R.setUTCFullYear(Na)}N==ha&&R.setUTCDate(R.getUTCDate()-R.getUTCDay()+t.startOfWeek);ya=1;s=Z=R.getTime()/oa;Na=R.getUTCFullYear();for(y=R.getUTCMonth();s<aa&&ya<100;){Da.push(s);if(N==qa)s=Date.UTC(Na+ya*S,0)/oa;else if(N==ua)s=Date.UTC(Na,
y+ya*S)/oa;else s+=N*S;ya++}aa=s;t.labels.formatter||(cb=function(){return pb(t.dateTimeLabelFormats[wa[0]],this.value,1)})}function F(){Da=[];if(!Ca){Z-=Z>=0?Z%ea:ea+Z%ea;if(aa%ea)aa+=ea-aa%ea}for(var s=(Ya<1?1/Ya:1)*10,y=Z;y<=aa;y+=ea)Da.push(K(y*s)/s);if(Ca){Z-=0.5;aa+=0.5}cb||(cb=function(){return this.value})}function w(){if(Z===null)Z=t.min===null?p.axes[fa+"Min"]:t.min;if(aa===null)aa=t.max===null?p.axes[fa+"Max"]:t.max;ea=Ca?1:t.tickInterval=="auto"?(aa-Z)*t.tickPixelInterval/Ha:t.tickInterval;
if(t.type!="datetime")ea=D(ea);db=t.minorTickInterval=="auto"&&ea?ea/5:t.minorTickInterval;t.type=="datetime"?A():F();Sa=Ha/(aa-Z)}function B(s,y){var M;if(y-s>t.maxZoom){Z=s;aa=y}else{M=(t.maxZoom-y+s)/2;Z=s-M;aa=y+M}w()}function I(){Z=aa=ea=db=Da=null;w()}function ma(){La.clear();x.alternateGridColor&&z(Da,function(M,H){if(H%2==0&&M<aa)J(M,Da[H+1]!==ba?Da[H+1]:aa,x.alternateGridColor)});x.plotBands&&z(x.plotBands,function(){J(this.from,this.to,this.color)});if(db&&!Ca)for(var s=Z;s<=aa;s+=db){L(s,
x.minorGridLineColor,x.minorGridLineWidth);x.minorTickWidth&&P(s,x.minorTickPosition,x.minorTickColor,x.minorTickWidth,x.minorTickLength)}z(Da,function(M){var H=M+Ma;L(H,x.gridLineColor,x.gridLineWidth);P(M,x.tickPosition,x.tickColor,x.tickWidth,x.tickLength,!(M==Z&&!x.showFirstLabel||M==aa&&!x.showLastLabel))});if(x.lineWidth)La.drawLine(Q,n?T-la:Y,n?ca-va:Q,T-la,x.lineColor,x.lineWidth);if(x.title&&x.title.enabled&&x.title.text){s=n?Q:Y;var y=n?ta:na;s={low:s+(n?0:y),middle:s+y/2,high:s+(n?y:0)}[x.title.align];
y=(n?Y+na:Q)+(n?1:-1)*x.title.margin-(Ba?parseInt(x.title.style.fontSize||x.title.style.font.replace(/^[a-z ]+/,""))/3:0);La.addText(x.title.text,n?s:y,n?y:s,x.title.style,x.title.rotation||0,{low:"left",middle:"center",high:"right"}[x.title.align])}La.strokeText()}if(Za)n=!n;var x={};for(var v in t)x[v]=t[v];var fa=n?Za?"y":"x":Za?"x":"y";v=fa=="x";var Ha=n?ta:na,Sa,bb=n?Q:la,La=new da("axis-layer",E,null,{zIndex:4}),aa=null,Z=null,ea,db,Ya,Da,cb=t.labels.formatter,Ca=t.categories||v&&p.columnCount,
kb=t.reversed,Ma=Ca&&t.tickmarkPlacement=="between"?0.5:0;this.render=ma;this.addPlotLine=L;this.translate=C;this.setExtremes=B;this.reset=I;this.categories=Ca;w()}function m(){function p(L,J,P,D){if(!C[L]){J=X(Ia,{innerHTML:J,title:P,onclick:D},W(a.toolbar.itemStyle,{zIndex:1003}),n.div);C[L]=J}}function t(L){C[L].parentNode.removeChild(C[L]);C[L]=null}var n,C={};n=new da("toolbar",E,null,{zIndex:1004,width:"auto",height:"auto"});return{add:p,remove:t}}function k(p,t){function n(){J.onmousemove=
D.onmousemove=function(v){v=v?v:Ea.event;v.returnValue=false;if(F){if(ma){var fa=v.clientX-w-Oa.x-Q;ga(I,{width:ra(fa)+G,left:(fa>0?w:w+fa)+G})}if(x){v=v.clientY-B-Oa.y-Y;ga(I,{height:ra(v)+G,top:(v>0?B:B+v)+G})}}else C(v)};J.onmouseout=D.onmouseout=function(v){v=v?v:Ea.event;if((v=v.relatedTarget||v.toElement)&&v!=D&&v.tagName!="AREA"){P.hide();if(p.hoverSeries){p.hoverSeries.setState();A=p.hoverSeries=null}}};J.onmousedown=D.onmousedown=function(v){v=v?v:Ea.event;if(ma||x){v.preventDefault&&v.preventDefault();
F=true;w=v.clientX-Oa.x-Q;B=v.clientY-Oa.y-Y;I||(I=X(Ia,null,{position:ja,border:"none",background:"#4572A7",opacity:0.25,width:ma?0:ta+G,height:x?0:na+G}));Ra.div.appendChild(I)}};J.onmouseup=D.onmouseup=function(){var v;v=p.xAxis.translate;var fa=p.yAxis.translate;F=false;if(I&&I.offsetWidth>10&&I.offsetHeight>10){v={xMin:v(I.offsetLeft,true),xMax:v(I.offsetLeft+I.offsetWidth,true),yMin:fa(na-I.offsetTop-I.offsetHeight,true),yMax:fa(na-I.offsetTop,true)};I.parentNode.removeChild(I);I=null;p.toolbar.add("zoom",
"Reset zoom","Reset zoom level 1:1",function(){p.zoom(false);p.toolbar.remove("zoom")});p.zoom(v)}}}function C(v){var fa=p.hoverPoint,Ha=p.hoverSeries;if(Ha){fa||(fa=Ha.tooltipPoints[Za?v.clientY-Oa.y-Y:v.clientX-Oa.x-Q]);if(fa!=A){P.refresh(fa,Ha);A=fa}}}function L(){var v="highchartsMap"+Gb++;D=X("img",{useMap:"#"+v},{width:ta+G,height:na+G,left:Q+G,top:Y+G,opacity:0,border:"none",position:ja,clip:"rect(1px,"+ta+"px,"+na+"px,1px)",zIndex:9},E);if(!Ba)D.src="data:image/gif;base64,R0lGODlhAQABAJH/AP///0////";
return X("map",{name:v,id:v},null,E)}if(t.enabled){var J,P=o(t),D,A,F,w,B,I,ma=/x/.test(p.options.chart.zoomType),x=/y/.test(p.options.chart.zoomType);this.imagemap=J=L();this.tooltip=P;this.zoomX=ma;this.zoomY=x;n();setInterval(function(){lb&&lb()},13)}}function o(p){function t(A,F){var w=A.tooltipPos,B=r.options,I=p.borderColor||A.color||F.color||"#606060";B=B.chart.inverted;var ma,x,v;ma=A.tooltipText;L=F;if(ma===false)C();else{J.innerHTML=ma;ma=w?w[0]:B?ta-A.y:A.x;w=w?w[1]:B?na-A.x:A.y;x=J.offsetWidth-
P;v=J.offsetHeight-P;if(x>(D.w||0)+20||x<(D.w||0)-20||v>D.h||D.c!=I){D.clear();D.drawRect(P/2,P/2,x+20,v,I,P,p.borderRadius,p.backgroundColor,p.shadow);W(D,{w:x,h:v,c:I})}I=ma-D.w+Q-35;if((B||I<5)&&ma+D.w<ca-100)I=ma+Q+15;B=w-D.h+10+Y;if(B<5)B=5;else if(B+D.h>T)B=T-D.h-5;n(K(I),K(B));F.drawPointState(A,"hover");za.style.visibility=eb}}function n(A,F){var w=za.style.visibility==pa,B=w?A:(za.offsetLeft+A)/2;w=w?F:(za.offsetTop+F)/2;ga(za,{left:B+G,top:w+G});if(ra(A-B)>1||ra(F-w)>1)n(A,F);else lb=null}
function C(){if(za)za.style.visibility=pa;L&&L.drawPointState()}var L,J,P=p.borderWidth,D;za=X(Ia,null,{position:ja,visibility:pa,overflow:pa,padding:"0 50px 5px 0",zIndex:6},E);D=new da("tooltip-box",za,null,{width:ta+G,height:na+G});J=X(Ia,{className:"highcharts-tooltip"},W(p.style,{position:sb,zIndex:2}),za);return{refresh:t,hide:C}}var u=function(p){if(!p.legend){var t,n=p.options.legend,C=n.layout,L=n.symbolWidth,J,P=[],D=new da("legend",E,null,{zIndex:5});if(n.enabled){J=X(Ia,{className:"highcharts-legend highcharts-legend-"+
C,innerHTML:'<ul style="margin:0;padding:0"></ul>'},W({position:ja,zIndex:10},n.style),E);z(p.series,function(A){if(A.options.showInLegend){var F=A.options.legendType=="point"?A.data:[A];z(F,function(w){w.simpleSymbol=/(bar|pie|area|column)/.test(A.type);var B=W(n.itemStyle,{paddingLeft:L+n.symbolPadding+G,cssFloat:C=="horizontal"?"left":"none"});w.legendItem=t=X("li",{innerHTML:n.labelFormatter.call(w),className:w.visible?"":pa},w.visible?B:V(B,n.itemHiddenStyle),J.firstChild);Ta(t,"mouseover",function(){w.visible&&
ga(this,n.itemHoverStyle);w.setState("hover")});Ta(t,"mouseout",function(){w.visible&&ga(this,B);w.setState()});Ta(t,"click",function(){var I=this.className;ga(this,I?B:n.itemHiddenStyle);w.setVisible(I)});P.push(w)})}});if(n.borderWidth||n.backgroundColor)D.drawRect(J.offsetLeft,J.offsetTop,J.offsetWidth,J.offsetHeight,n.borderColor,n.borderWidth,n.borderRadius,n.backgroundColor,n.shadow);z(P,function(A){var F=A.legendItem,w=J.offsetLeft+F.offsetLeft;F=J.offsetTop+F.offsetTop+F.offsetHeight/2;!A.simpleSymbol&&
A.options&&A.options.lineWidth&&D.drawLine(w,F,w+L,F,A.color,A.options.lineWidth);if(A.simpleSymbol)D.drawRect(w,F-6,16,12,null,0,2,A.color);else A.options&&A.options.marker&&A.options.marker.enabled&&A.drawMarker(D,w+L/2,F,A.options.marker)})}}};if(a.chart&&a.chart.inverted)Aa=V(Aa,Hb);a=V(Aa,a);var q=a.chart,O=q.margin;if(typeof O=="number")O=[O,O,O,O];var r=this,E=Fa.getElementById(q.renderTo),Y=O[0],va=O[1],la=O[2],Q=O[3],ka=new da("chart-background",E),T,ca,Ra,na,ta,Wa,Xa,Ga=[],Oa=qb(E),ab,Za,
lb,za,Va;fb=Pa=0;Ta(Ea,"resize",function(){Oa=qb(E)});r.addLoading=function(p){r.resources[p]=false};r.clearLoading=function(p){r.resources[p]=true;d()};r.options=a;r.series=Ga;r.resources={};r.inverted=Za=a.chart.inverted;r.chartWidth=ca=E.offsetWidth;r.chartHeight=T=E.offsetHeight;r.plotWidth=ta=ca-Q-va;r.plotHeight=na=T-Y-la;r.plotLeft=Q;r.plotTop=Y;r.stacks=Va={bar:[],column:[],area:[],areaspline:[]};r.plotLayer=Ra=new da("plot",E,null,{position:ja,width:ta+G,height:na+G,left:Q+G,top:Y+G,overflow:pa,
zIndex:6});this.tracker=new k(r,a.tooltip);if(q.plotBackgroundImage){r.addLoading("plotBack");ab=X("img");ab.onload=function(){r.clearLoading("plotBack")};ab.src=q.plotBackgroundImage}b();d()}function tb(a){for(var b=[],c=[],d=0;d<a.length;d++){b[d]=a[d].x;c[d]=a[d].y}this.xdata=b;this.ydata=c;a=[];this.y2=[];var e=c.length;this.n=e;this.y2[0]=0;this.y2[e-1]=0;a[0]=0;for(d=1;d<e-1;d++){var f=b[d+1]-b[d-1];f=(b[d]-b[d-1])/f;var j=f*this.y2[d-1]+2;this.y2[d]=(f-1)/j;a[d]=(c[d+1]-c[d])/(b[d+1]-b[d])-
(c[d]-c[d-1])/(b[d]-b[d-1]);a[d]=(6*a[d]/(b[d+1]-b[d-1])-f*a[d-1])/j}for(b=e-2;b>=0;b--)this.y2[b]=this.y2[b]*this.y2[b+1]+a[b]}var ba,Fa=document,Ea=window,ia=Math,K=ia.round,xa=ia.floor,ra=ia.abs,gb=ia.cos,hb=ia.sin,U=navigator.userAgent,Ba=/msie/i.test(U)&&!Ea.opera,Ib=/AppleWebKit/.test(U),Gb=0,Pa,fb,ub={},vb=0,oa=1,Ia="div",ja="absolute",sb="relative",pa="hidden",eb="visible",G="px",z,Qa,V,mb,Ta,ib,nb;if(Ea.jQuery){var Ua=jQuery;z=function(a,b){for(var c=0,d=a.length;c<d;c++)if(b.call(a[c],a[c],
c,a)===false)return c};Qa=function(a,b){for(var c=[],d=0,e=a.length;d<e;d++)c[d]=b.call(a[d],a[d],d,a);return c};V=function(){var a=arguments;return Ua.extend(true,null,a[0],a[1],a[2],a[3])};mb=function(a){return a.replace(/([A-Z])/g,function(b,c){return"-"+c.toLowerCase()})};Ta=function(a,b,c){Ua(a).bind(b,c)};Ua.extend(Ua.easing,{easeOutQuad:function(a,b,c,d,e){return-d*(b/=e)*(b-2)+c}});ib=function(a,b,c){Ua(a).animate(b,c)};nb=function(a,b){Ua.get(a,null,b)}}else if(Ea.MooTools){z=function(a,
b){a.each(b)};Qa=function(a,b){return a.map(b)};V=function(){if(Ea.$merge)return $merge.apply(this,arguments)};mb=function(a){return a.hyphenate()};Ta=function(a,b,c){a.addEvent(b,c)};ib=function(a,b,c){a=new Fx.Morph($(a),W(c,{transition:Fx.Transitions.Quad.easeInOut}));a.start(b)};nb=function(a,b){(new Request({url:a,method:"get",onSuccess:b})).send()}}U='normal 12px "Lucida Grande", "Lucida Sans Unicode", Verdana, Arial, Helvetica, sans-serif';var Ja={enabled:true,align:"center",x:0,y:15,style:{color:"#666",
font:U.replace("12px","11px")}},Aa={colors:["#4572A7","#AA4643","#89A54E","#80699B","#3D96AE","#DB843D","#92A8CD","#A47D7C","#B5CA92"],symbols:["circle","diamond","square","triangle","triangle-down"],lang:{months:["January","February","March","April","May","June","July","August","September","October","November","December"],weekdays:["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"]},chart:{margin:[50,50,60,80],borderColor:"#4572A7",borderRadius:5,defaultSeriesType:"line",plotBorderColor:"#C0C0C0"},
title:{text:"Chart title",style:{textAlign:"center",color:"#3E576F",font:U.replace("12px","16px"),margin:"10px 0 0 0"}},subtitle:{text:"",style:{textAlign:"center",color:"#6D869F",font:U,margin:0}},xAxis:{dateTimeLabelFormats:{second:"%H:%M:%S",minute:"%H:%M",hour:"%H:%M",day:"%e. %b",week:"%e. %b",month:"%b '%y",year:"%Y"},gridLineColor:"#C0C0C0",labels:Ja,lineColor:"#C0D0E0",lineWidth:1,max:null,min:null,maxZoom:1,minorGridLineColor:"#E0E0E0",minorGridLineWidth:1,minorTickColor:"#A0A0A0",minorTickLength:2,
minorTickPosition:"outside",minorTickWidth:1,showFirstLabel:true,showLastLabel:false,startOfWeek:1,tickColor:"#C0D0E0",tickInterval:"auto",tickLength:5,tickmarkPlacement:"between",tickPixelInterval:100,tickPosition:"outside",tickWidth:1,title:{enabled:false,text:"X-values",align:"middle",margin:35,style:{color:"#6D869F",font:U.replace("normal","bold")}},type:"linear"},plotOptions:{line:{lineWidth:2,shadow:true,marker:{enabled:true,symbol:"auto",lineWidth:0,radius:4,lineColor:"#FFFFFF",fillColor:"auto",
states:{hover:{}}},dataLabels:V(Ja,{enabled:false,y:-6,formatter:function(){return this.y}}),showInLegend:true,states:{hover:{lineWidth:3,marker:{}}}}},labels:{style:{position:ja,color:"#3E576F",font:U}},legend:{enabled:true,layout:"horizontal",labelFormatter:function(){return this.name},borderColor:"#909090",borderRadius:5,shadow:true,style:{position:ja,zIndex:10,bottom:"10px",left:"80px",padding:"5px"},itemStyle:{listStyle:"none",margin:"0 1em 0 0",padding:0,font:U,cursor:"pointer",color:"#3E576F"},
itemHoverStyle:{color:"#000"},itemHiddenStyle:{color:"#CCC"},symbolWidth:16,symbolPadding:5},tooltip:{enabled:true,formatter:function(){return"<b>"+(this.point.name||this.series.name)+"</b><br/>X value: "+this.x+"<br/>Y value: "+this.y},backgroundColor:"rgba(255, 255, 255, .85)",borderWidth:2,borderRadius:5,shadow:true,style:{color:"#333333",fontSize:"9pt",padding:"5px",font:U}},toolbar:{itemStyle:{color:"#4572A7",cursor:"pointer",margin:"20px",font:U}},credits:{enabled:true,text:"",
href:"http://www.highcharts.com",style:{position:ja,right:"50px",bottom:"5px",color:"#999",textDecoration:"none",font:U.replace("12px","10px")}}};Aa.yAxis=V(Aa.xAxis,{gridLineWidth:1,tickPixelInterval:72,showLastLabel:true,labels:{align:"right",x:-8,y:3},lineWidth:0,rotation:270,tickWidth:0,title:{enabled:true,margin:40,rotation:270,text:"Y-values"}});U=Aa.plotOptions;Ja=U.line;U.spline=V(Ja);U.scatter=V(Ja,{lineWidth:0,states:{hover:{lineWidth:0}}});U.area=V(Ja,{fillColor:"auto"});U.areaspline=V(U.area);
U.column=V(Ja,{borderColor:"#FFFFFF",borderWidth:1,borderRadius:0,groupPadding:0.2,pointPadding:0.1,states:{hover:{brightness:0.1,shadow:false}}});U.bar=V(U.column,{dataLabels:{align:"left",x:5,y:0}});U.pie=V(Ja,{center:["50%","50%"],legendType:"point",size:"90%",slicedOffset:10,states:{hover:{brightness:0.1,shadow:false}}});var Hb={xAxis:{reversed:true,labels:{align:"right",x:-8,y:3},title:{rotation:270}},yAxis:{labels:{align:"center",x:0,y:14},title:{rotation:0}}},ob=function(a){function b(g){if(j=
/rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]+(?:\.[0-9]+)?)\s*\)/.exec(g))f=[parseInt(j[1]),parseInt(j[2]),parseInt(j[3]),parseFloat(j[4])];else if(j=/#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/.exec(g))f=[parseInt(j[1],16),parseInt(j[2],16),parseInt(j[3],16),1]}function c(){return f?"rgba("+f.join(",")+")":a}function d(g){if(typeof g=="number"&&g!=0)for(var l=0;l<3;l++){f[l]+=parseInt(g*255);if(f[l]<0)f[l]=0;if(f[l]>255)f[l]=255}return this}function e(g){f[3]=
g;return this}var f,j;b(a);return{get:c,brighten:d,setOpacity:e}},da=function(a,b,c,d){var e=this;c=W({className:"highcharts-"+a},c);d=W({width:b.offsetWidth+G,height:b.offsetHeight+G,position:ja,top:0,left:0,margin:0,padding:0,border:"none"},d);a=X(Ia,c,d,b);W(e,{div:a,width:a.offsetWidth,height:a.offsetHeight});e.svg=Ba?"":'<?xml version="1.0" encoding="utf-8"?><svg version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="'+e.width+'px" height="'+e.height+
'">'};da.prototype={getCtx:function(){if(!this.ctx){var a=X("canvas",{id:"highcharts-canvas-"+vb++,width:this.width,height:this.height},{position:ja},this.div);if(Ba){G_vmlCanvasManager.initElement(a);a=Fa.getElementById(a.id)}this.ctx=a.getContext("2d")}return this.ctx},getSvg:function(){if(!this.svgObject){var a=this,b=a.div,c=a.width;a=a.height;if(Ba){if(!Fa.namespaces.g_vml_){Fa.namespaces.add("g_vml_","urn:schemas-microsoft-com:vml");Fa.createStyleSheet().cssText="g_vml_\\:*{behavior:url(#default#VML)}"}this.svgObject=
X(Ia,null,{width:c+G,height:a+G,position:ja},b)}else this.svgObject=X("object",{width:c,height:a,type:"image/svg+xml"},{position:ja,left:0,top:0},b)}return this.svgObject},drawLine:function(a,b,c,d,e,f){var j=this.getCtx();if(a==c)a=c=K(a)+f%2/2;if(b==d)b=d=K(b)+f%2/2;j.lineWidth=f;j.lineCap="round";j.beginPath();j.moveTo(a,b);j.strokeStyle=e;j.lineTo(c,d);j.closePath();j.stroke()},drawPolyLine:function(a,b,c,d,e){var f=this.getCtx(),j=[];if(d&&c){z(a,function(g){j.push(g===ba?g:g+1)});for(d=1;d<=
3;d++)this.drawPolyLine(j,"rgba(0, 0, 0, "+0.05*d+")",6-2*d)}f.beginPath();for(d=0;d<a.length;d+=2)f[d==0?"moveTo":"lineTo"](a[d],a[d+1]);W(f,{lineWidth:c,lineJoin:"round"});if(b&&c){f.strokeStyle=b;f.stroke()}if(e){f.fillStyle=e;f.fill()}},drawRect:function(a,b,c,d,e,f,j,g,l,m){function k(){o.beginPath();if(j){o.moveTo(a,b+j);o.lineTo(a,b+d-j);o.quadraticCurveTo(a,b+d,a+j,b+d);o.lineTo(a+c-j,b+d);o.quadraticCurveTo(a+c,b+d,a+c,b+d-j);o.lineTo(a+c,b+j);o.quadraticCurveTo(a+c,b,a+c-j,b);o.lineTo(a+
j,b);o.quadraticCurveTo(a,b,a,b+j)}else o.rect(a,b,c,d);o.closePath()}var o=this.getCtx(),u=(f||0)%2/2;a=K(a)+u;b=K(b)+u;c=K(c);d=K(d);if(l)for(l=1;l<=3;l++)this.drawRect(a+1,b+1,c,d,"rgba(0, 0, 0, "+0.05*l+")",6-2*l,j);m&&o.drawImage(m,a,b,c,d);k();if(g){o.fillStyle=xb(g,o);o.fill();Ea.G_vmlCanvasManager&&k()}if(f){o.strokeStyle=e;o.lineWidth=f;o.stroke()}},drawSymbol:function(a,b,c,d,e,f,j){var g=this.getCtx(),l=/^url\((.*?)\)$/;g.beginPath();if(a=="square"){a=0.707*d;g.moveTo(b-a,c-a);g.lineTo(b+
a,c-a);g.lineTo(b+a,c+a);g.lineTo(b-a,c+a);g.lineTo(b-a,c-a)}else if(a=="triangle"){c++;g.moveTo(b,c-1.33*d);g.lineTo(b+d,c+0.67*d);g.lineTo(b-d,c+0.67*d);g.lineTo(b,c-1.33*d)}else if(a=="triangle-down"){c--;g.moveTo(b,c+1.33*d);g.lineTo(b-d,c-0.67*d);g.lineTo(b+d,c-0.67*d);g.lineTo(b,c+1.33*d)}else if(a=="diamond"){g.moveTo(b,c-d);g.lineTo(b+d,c);g.lineTo(b,c+d);g.lineTo(b-d,c);g.lineTo(b,c-d)}else l.test(a)?X("img",{onload:function(){var m=this,k=ub[m.src]||[m.width,m.height];ga(m,{left:K(b-k[0]/
2)+G,top:K(c-k[1]/2)+G,visibility:eb});ub[m.src]=k},src:a.match(l)[1]},{position:ja,visibility:Ba?eb:pa},this.div):g.arc(b,c,d,0,2*ia.PI,true);if(j){g.fillStyle=j;g.fill()}if(f&&e){g.strokeStyle=f||"rgb(100, 100, 255)";g.lineWidth=e||2;g.stroke()}},drawHtml:function(a,b,c){X(Ia,W(b,{innerHTML:a}),W(c,{position:ja}),this.div)},drawText:function(){this.addText.apply(this,arguments);this.strokeText()},addText:function(a,b,c,d,e,f){if(a||a===0){var j=this,g,l=j.div,m,k="";d=d||{};var o=d.color||"#000000";
f=f||"left";var u=parseInt(d.fontSize||d.font.replace(/^[a-z ]+/,""));for(var q in d)k+=mb(q)+":"+d[q]+";";z(["MozTransform","WebkitTransform","transform"],function(Y){if(Y in l.style)m=Y});if(!e||m){a=X("span",{innerHTML:a},W(d,{position:ja,left:b+G,whiteSpace:"nowrap",bottom:K(j.height-c-u*0.25)+G,color:o}),l);k=a.offsetWidth;if(f=="right")ga(a,{left:b-k+G});else f=="center"&&ga(a,{left:K(b-k/2)+G});if(e){f={left:0,center:50,right:100}[f];a.style[m]="rotate("+e+"deg)";a.style[m+"Origin"]=f+"% 100%"}}else if(Ba){g=
true;d=(e||0)*ia.PI*2/360;e=gb(d);d=hb(d);q=j.width;u=u/3||3;var O=f=="left",r=f=="right",E=O?b:b-q*e;b=r?b:b+q*e;O=O?c:c-q*d;c=r?c:c+q*d;E+=u*d;b+=u*d;O-=u*e;c-=u*e;if(ra(E-b)<0.1)E+=0.1;if(ra(O-c)<0.1)O+=0.1;j.svg+='<g_vml_:line from="'+E+", "+O+'" to="'+b+", "+c+'" stroked="false"><g_vml_:fill on="true" color="'+o+'"/><g_vml_:path textpathok="true"/><g_vml_:textpath on="true" string="'+a+'" style="v-text-align:'+f+";"+k+'"/></g_vml_:line>'}else{g=true;j.svg+='<g><text transform="translate('+b+
","+c+") rotate("+(e||0)+')" style="fill:'+o+";text-anchor:"+{left:"start",center:"middle",right:"end"}[f]+";"+k.replace(/"/g,"'")+'">'+a+"</text></g>"}j.hasObject=g}},strokeText:function(){if(this.hasObject){var a=this.getSvg(),b=this.svg;if(Ba)a.innerHTML=b;else{a.data="data:image/svg+xml,"+b+"</svg>";Ib&&this.div.appendChild(a)}}},clear:function(){var a=this,b=this.div,c=b.childNodes;a.ctx&&a.ctx.clearRect(0,0,a.width,a.height);if(a.svgObject){b.removeChild(a.svgObject);a.svgObject=null}for(var d=
c.length-1;d>=0;d--){a=c[d];a.tagName=="SPAN"&&b.removeChild(a)}},hide:function(){ga(this.div,{visibility:pa,display:"none"})},show:function(){ga(this.div,{visibility:eb,display:""})}};var sa=function(){this.needsAxes=true;this.type="line"};sa.prototype={init:function(a,b){var c=this;c.chart=a;b=c.setOptions(b);W(c,{options:b,name:b.name,state:"",visible:b.visible||b.visible===ba});c.getColor();c.getSymbol();c.getData(b)},getData:function(a){var b=this,c=b.chart,d="series"+vb++;if(!a.data&&a.dataURL){c.addLoading(d);
nb(a.dataURL,function(e){b.dataLoaded(e);c.clearLoading(d)})}else b.dataLoaded(a.data)},dataLoaded:function(a){var b=this,c=b.chart,d=b.options,e=d.dataParser,f={},j,g;if(d.dataURL&&!e)e=function(k){return eval(k)};if(e)a=e.call(b,a);this.layerGroup=j=new da("series-group",c.plotLayer.div,null,{zIndex:2});z(["","hover"],function(k){f[k]=new da("state-"+k,j.div);k&&f[k].hide()});this.stateLayers=f;b.visible||b.setVisible(false);g=d.pointStart||0;a=Qa(a,function(k){var o=k,u;if(typeof k=="number"||
k===null)o=[g,k];else if(typeof k=="object"&&typeof k.length!="number"){o=[k.x===ba?g:k.x,k.y];for(u in k)o[u]=k[u]}else if(typeof k[0]=="string"){k.name=k[0];k[0]=g;o=k}g+=d.pointInterval||1;return o});b.data=c.options.xAxis.reversed?$a(a):a;var l=-1,m=[];z(a,function(k,o){if(k[1]===null){o>l+1&&m.push(a.slice(l+1,o));l=o}else o==a.length-1&&m.push(a.slice(l+1,o+1))});this.segments=m},setOptions:function(a){return V(this.chart.options.plotOptions[this.type],a)},getColor:function(){var a=this.chart.options.colors;
this.color=this.options.color||a[Pa++]||"#0000ff";if(Pa>=a.length)Pa=0},getSymbol:function(){var a=this.chart.options.symbols,b=this.options.marker.symbol||"auto";if(b=="auto")b=a[fb++];this.symbol=b;if(fb>=a.length)fb=0},translate:function(){var a=this.chart,b=this,c=b.options.stacking,d=a.stacks[b.type];z(this.data,function(e){var f=e[0],j=e[1],g;e.x=a.xAxis.translate(e[0]);if(c){g=d[f];f=g.cum-=j;j=f+j;if(c=="percent"){f=f*100/g.total;j=j*100/g.total;e.percentage=e[1]*100/g.total}e.yBottom=a.yAxis.translate(f,
0,1)}if(j!==null)e.y=a.yAxis.translate(j,0,1);e.clientX=a.inverted?a.plotHeight-e.x+a.plotTop:e.x+a.plotLeft});this.setTooltipPoints()},setTooltipPoints:function(){var a=this,b=a.chart,c=b.inverted,d=b.xAxis.categories,e=[],f=c?b.plotHeight:b.plotWidth,j,g,l=[];z(a.segments,function(m){e=e.concat(m)});if(b.options.xAxis.reversed)e=$a(e);z(e,function(m,k){m.tooltipText=b.options.tooltip.formatter.call({series:a,point:m,x:d&&d[m[0]]!==ba?d[m[0]]:m[0],y:m[1],percentage:m.percentage});j=e[k-1]?e[k-1].high+
1:0;for(g=m.high=e[k+1]?xa((m.x+(e[k+1]?e[k+1].x:f))/2):f;j<=g;)l[c?f-j++:j++]=m});a.tooltipPoints=l},drawLine:function(a){var b=this,c=b.options,d=b.chart,e=b.stateLayers[a],f=c.lineColor||b.color,j=c.fillColor=="auto"?ob(b.color).setOpacity(c.fillOpacity||0.75).get():c.fillColor,g=d.inverted,l=(g?0:d.chartHeight)-d.yAxis.translate(0);if(a)c=V(c,c.states[a]);b.animate&&b.animate(true);z(b.segments,function(m){var k=[],o=[];z(m,function(q){k.push(g?d.plotWidth-q.y:q.x,g?d.plotHeight-q.x:q.y)});if(/area/.test(b.type)){for(var u=
0;u<k.length;u++)o.push(k[u]);if(c.stacking&&b.type!="areaspline")for(u=m.length-1;u>=0;u--)o.push(m[u].x,m[u].yBottom);else o.push(g?l:m[m.length-1].x,g?m[0].x:l,g?l:m[0].x,g?m[m.length-1].x:l);e.drawPolyLine(o,null,null,c.shadow,j)}c.lineWidth&&e.drawPolyLine(k,f,c.lineWidth,c.shadow)});b.animate&&b.animate()},animate:function(a){var b=this,c=b.layerGroup.div;if(a)ga(c,{overflow:pa,width:0});else{ib(c,{width:b.chart.plotWidth+G},{duration:1E3});this.animate=null}},drawPoints:function(a){var b=this,
c=b.stateLayers[a],d=b.options,e=d.marker,f=b.data,j=b.chart,g=j.inverted;if(a){a=d.states[a].marker;if(a.lineWidth===ba)a.lineWidth=e.lineWidth+1;if(a.radius===ba)a.radius=e.radius+1;e=V(e,a)}e.enabled&&z(f,function(l){if(l.y!==ba)b.drawMarker(c,g?j.plotWidth-l.y:l.x,g?j.plotHeight-l.x:l.y,V(e,l.marker))})},drawMarker:function(a,b,c,d){if(d.lineColor=="auto")d.lineColor=this.color;if(d.fillColor=="auto")d.fillColor=this.color;if(d.symbol=="auto")d.symbol=this.symbol;a.drawSymbol(d.symbol,b,c,d.radius,
d.lineWidth,d.lineColor,d.fillColor)},drawDataLabels:function(){if(this.options.dataLabels.enabled&&!this.hasDrawnDataLabels){var a=this,b,c,d=a.data,e=a.options.dataLabels,f,j,g=a.chart,l=g.inverted,m=a.type=="pie";a.dataLabelsLayer=j=new da("data-labels",a.layerGroup.div,null,{zIndex:1});e.style.color=e.color=="auto"?a.color:e.color;z(d,function(k){f=e.formatter.call({x:k[0],y:k[1],series:a,point:k});b=(l?g.plotWidth-k.y:k.x)+e.x;c=(l?g.plotHeight-k.x:k.y)+e.y;if(k.tooltipPos){b=k.tooltipPos[0]+
e.x;c=k.tooltipPos[1]+e.y}if(m)j=new da("data-labels",k.layer.div,null,{zIndex:3});if(f)j[m?"drawText":"addText"](f,b,c,e.style,e.rotation,e.align)});m||j.strokeText();a.hasDrawnDataLabels=true}},drawPointState:function(a,b){var c=this.chart,d=c.inverted,e=c.singlePointLayer,f=this.options;if(!e)e=c.singlePointLayer=new da("single-point",c.plotLayer.div,null,{zIndex:3});e.clear();if(b){var j=f.states[b].marker;b=f.marker.states[b];if(b.radius===ba)b.radius=j.radius+2;if((f=V(f.marker,a.marker,j,b))&&
f.enabled)this.drawMarker(e,d?c.plotWidth-a.y:a.x,d?c.plotHeight-a.x:a.y,f)}},render:function(){this.drawDataLabels();for(var a in this.stateLayers){this.drawLine(a);this.drawPoints(a)}},clear:function(){var a=this.stateLayers;for(var b in a){a[b].clear();a[b].cleared=true}if(this.dataLabelsLayer){this.dataLabelsLayer.clear();this.hasDrawnDataLabels=false}},setState:function(a){a=a||"";if(this.state!=a){var b=this,c=b.stateLayers,d=c[a];c=c[b.state];var e=b.singlePointLayer||b.chart.singlePointLayer;
if(b.state=a)d.show();else{c.hide();e&&e.clear()}}},setVisible:function(a){this.visible=a?true:false;a?this.layerGroup.show():this.layerGroup.hide();if(this.legendItem)this.legendItem.className=a?"":pa},getAreaCoords:function(){var a=this.chart,b=a.inverted,c=a.plotWidth,d=a.plotHeight,e=10,f=[];z(this.segments,function(j,g){if(a.options.xAxis.reversed)j=$a(j);var l=[],m=[],k=[];z([m,k],function(o){for(var u=0,q=0,O,r,E=[j[0]],Y=o==m?1:-1,va,la,Q,ka,T,ca;j[q];){if(j[q].x>j[u].x+e||q==j.length-1){O=
j[q];r=j.slice(u,q-1);z(r,function(Ra){if(Y*Ra.y<Y*O.y)O=Ra});K(j[u].x)<K(O.x)&&E.push(O);u=q}q++}E.push(j[j.length-1]);for(q=0;q<E.length;q++)if(q>0){la=E[q].x;va=E[q].y;u=la-E[q-1].x;Q=r=va-E[q-1].y;ka=-u;u=ia.sqrt(ia.pow(Q,2)+ia.pow(ka,2));T=Y*e/u;u=K(E[q-1].x+T*Q);r=K(E[q-1].y+T*ka);la=K(la+T*Q);Q=K(va+T*ka);if(o[o.length-1]&&o[o.length-1][0]>u)for(va=false;!va;){ca=o.pop();ka=o[o.length-1];if(!ka)break;T=(r-Q)/(u-la);ca=(ka[1]-ca[1])/(ka[0]-ca[0]);ca=(-ca*ka[0]+ka[1]+T*u-r)/(T-ca);T=T*(ca-u)+
r;if(ca>ka[0]){o.push([K(ca),K(T),1]);va=true}}else isNaN(u)||o.push([u,r]);o[o.length-1]&&o[o.length-1][0]<la&&o.push([la,Q])}});for(g=0;g<m.length;g++)l.push(b?c-m[g][1]:m[g][0],b?d-m[g][0]:m[g][1]);for(g=k.length-1;g>=0;g--)l.push(b?c-k[g][1]:k[g][0],b?d-k[g][0]:k[g][1]);l.length||l.push(K(j[0].x),K(j[0].y));f.push([l.join(",")])});return f},createArea:function(){var a,b=this,c=b.chart,d=b.getAreaCoords(),e=c.tracker.imagemap,f=e.firstChild,j=[],g;z(d,function(l){g=/^[0-9]+,[0-9]+$/.test(l[0]);
a=X("area",{shape:g?"circle":"poly",chart:c,coords:l[0]+(g?",10":""),onmouseover:function(){if(b.visible){var m=c.hoverSeries;c.hoverPoint=l[1];m&&m!=b&&m.setState();e.insertBefore(this,e.childNodes[0]);b.setState("hover");c.hoverSeries=b}}});f?e.insertBefore(a,f):e.appendChild(a);j.push(a)});b.areas=j}};var Ab=Ka(sa,{type:"area"}),rb=Ka(sa,{type:"spline",drawLine:function(){var a=this,b=a.segments;a.segments=a.splinedata||a.getSplineData();sa.prototype.drawLine.apply(a,arguments);a.segments=b},getSplineData:function(){var a=
this.chart,b=[],c;z(this.segments,function(d){if(a.options.xAxis.reversed)d=$a(d);var e=[];z(d,function(f,j){var g=d[j+2]||d[j+1]||f;j=d[j-2]||d[j-1]||f;g.x>0&&j.x<a.plotWidth&&e.push(f)});if(e.length>1)c=K(ia.max(a.plotWidth,e[e.length-1].clientX-e[0].clientX)/3);b.push(c?(new tb(e)).get(c):[])});return this.splinedata=b}}),Bb=Ka(rb,{type:"areaspline"}),jb=Ka(sa,{type:"column",init:function(){sa.prototype.init.apply(this,arguments);var a=this.chart;if(a.columnCount&&!this.options.stacking)a.columnCount++;
else a.columnCount=1;this.columnNumber=a.columnCount},translate:function(){sa.prototype.translate.apply(this);var a=this,b=a.options,c=a.data,d=a.chart,e=d.inverted,f=d.plotWidth,j=d.plotHeight,g=c[1]?c[1].x-c[0].x:e?j:f,l=g*b.groupPadding,m=g-2*l;m=m/d.columnCount;b=m*b.pointPadding;var k=m-2*b;a=d.options.xAxis.reversed?d.columnCount-a.columnNumber:a.columnNumber-1;var o=-(g/2)+l+a*m+b,u=d.yAxis.translate(0);z(c,function(q){q.x+=o;q.w=k;q.y0=(e?f:j)-u;q.h=(q.yBottom||q.y0)-q.y})},drawLine:function(){},
getSymbol:function(){},drawPoints:function(a){var b=this,c=b.options,d=b.chart,e=d.inverted,f=b.data;a=b.state;var j=b.stateLayers[a];b.animate&&this.animate(true);z(f,function(g){h=g.h;if(g.y!==ba)j.drawRect(e?d.plotWidth-g.y0:g.x,e?d.plotHeight-g.x-g.w:g.h>=0?g.y:g.y+g.h,e?g.h:g.w,e?g.w:ra(g.h),c.borderColor,c.borderWidth,c.borderRadius,b.color,c.shadow)});this.animate&&this.animate()},drawPointState:function(a,b){var c=this,d=c.chart,e=d.inverted,f=c.singlePointLayer;if(!f)f=c.singlePointLayer=
new da("single-point-layer",c.layerGroup.div);f.clear();if(b&&this.options.states[b]){b=V(this.options,this.options.states[b]);f.drawRect(e?d.plotWidth-a.y0:a.x,e?d.plotHeight-a.x-a.w:a.y,e?a.h:a.w,e?a.w:a.h,b.borderColor,b.borderWidth,b.borderRadius,ob(b.color||this.color).brighten(b.brightness).get(),b.shadow)}},getAreaCoords:function(){var a=[],b=this.chart,c=b.inverted;z(this.data,function(d){var e=c?b.plotWidth-d.y0:d.x,f=c?b.plotHeight-d.x-d.w:d.y,j=f+(c?d.w:d.h),g=e+(c?d.h:d.w);a.push([Qa([e,
j,e,f,g,f,g,j],K).join(","),d])});return a},animate:function(a){var b=this,c=b.chart,d=c.inverted;b=b.layerGroup.div;if(a)b.style[d?"left":"top"]=(d?-c.plotWidth:c.plotHeight)+G;else{ib(b,this.chart.inverted?{left:0}:{top:0});this.animate=null}}}),Cb=Ka(jb,{type:"bar",init:function(a){a.inverted=true;jb.prototype.init.apply(this,arguments)}}),Eb=Ka(sa,{type:"scatter",getAreaCoords:function(){var a=this.data,b=[];z(a,function(c){b.push([[K(c.x),K(c.y)].join(","),c])});return b}}),Db=Ka(sa,{type:"pie",
needsAxes:false,getColor:function(){},translate:function(){var a=0,b=this,c=-0.25,d=b.options,e=d.slicedOffset,f=d.center,j=b.chart,g=b.data,l=2*ia.PI,m=j.options.colors;f.push(d.size);f=Qa(f,function(k,o){return/%$/.test(k)?j["plot"+(o?"Height":"Width")]*parseInt(k)/100:k});z(g,function(k){a+=k[1]});z(g,function(k){k.start=c*l;c+=k[1]/a;k.end=c*l;k.center=[f[0],f[1]];k.size=f[2];var o=(k.end+k.start)/2;k.centerSliced=Qa([gb(o)*e+f[0],hb(o)*e+f[1]],K);if(!k.color)k.color=m[Pa++];if(Pa>=m.length)Pa=
0;if(k.visible===ba)k.visible=1;if(!k.layer)k.layer=new da("pie",b.layerGroup.div);k.setState=function(u){b.drawPointState(k,u)};k.setVisible=function(u){var q=u?"show":"hide",O=k.legendItem;k.visible=u;k.layer[q]();if(O)O.className=u?"":pa}});this.setTooltipPoints()},render:function(){this.pointsDrawn||this.drawPoints();this.drawDataLabels()},drawPoints:function(){var a=this;z(this.data,function(b){a.drawPoint(b,b.layer.getCtx(),b.color)});a.pointsDrawn=true},getSymbol:function(){},drawPointState:function(a,
b){var c=this,d=c.options,e;if(a){e=a.stateLayer;if(!e)e=a.stateLayer=new da("state-layer",a.layer.div);e.clear();if(b&&c.options.states[b]){b=V(d,d.states[b]);this.drawPoint(a,e.getCtx(),b.color||a.color,b.brightness)}}c.hoverPoint&&c.hoverPoint.stateLayer.clear();c.hoverPoint=a},drawPoint:function(a,b,c,d){var e=a.sliced?a.centerSliced:a.center,f=e[0];e=e[1];var j=a.size;b.fillStyle=ob(c).brighten(d).get(b);b.beginPath();b.moveTo(f,e);b.arc(f,e,j/2,a.start,a.end,false);b.lineTo(f,e);b.closePath();
b.fill()},getAreaCoords:function(){var a=[];z(this.data,function(b){for(var c=b.center[0],d=b.center[1],e=b.size/2,f=b.start,j=b.end,g=[],l=f;l;l+=0.25){if(l>=j)l=j;g=g.concat([c+gb(l)*e,d+hb(l)*e]);if(l>=j)break}g=g.concat([c,d]);b.tooltipPos=[c+2*gb((f+j)/2)*e/3,d+2*hb((f+j)/2)*e/3];a.push([Qa(g,K).join(","),b])});return a}});tb.prototype={get:function(a){a||(a=50);var b=this.n;b=(this.xdata[b-1]-this.xdata[0])/(a-1);var c=[],d=[];c[0]=this.xdata[0];d[0]=this.ydata[0];for(var e=[{x:c[0],y:d[0]}],
f=1;f<a;f++){c[f]=c[0]+f*b;d[f]=this.interpolate(c[f]);e[f]={x:c[f],y:d[f]}}return e},interpolate:function(a){for(var b=this.n-1,c=0;b-c>1;){var d=(b+c)/2;if(this.xdata[xa(d)]>a)b=d;else c=d}b=xa(b);c=xa(c);d=this.xdata[b]-this.xdata[c];var e=(this.xdata[b]-a)/d;a=(a-this.xdata[c])/d;return e*this.ydata[c]+a*this.ydata[b]+((e*e*e-e)*this.y2[c]+(a*a*a-a)*this.y2[b])*d*d/6}};Highcharts={numberFormat:yb,dateFormat:pb,setOptions:wb,Chart:zb}})();
