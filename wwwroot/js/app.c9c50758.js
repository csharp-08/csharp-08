(function(t){function e(e){for(var n,i,s=e[0],c=e[1],l=e[2],f=0,h=[];f<s.length;f++)i=s[f],Object.prototype.hasOwnProperty.call(r,i)&&r[i]&&h.push(r[i][0]),r[i]=0;for(n in c)Object.prototype.hasOwnProperty.call(c,n)&&(t[n]=c[n]);u&&u(e);while(h.length)h.shift()();return a.push.apply(a,l||[]),o()}function o(){for(var t,e=0;e<a.length;e++){for(var o=a[e],n=!0,s=1;s<o.length;s++){var c=o[s];0!==r[c]&&(n=!1)}n&&(a.splice(e--,1),t=i(i.s=o[0]))}return t}var n={},r={app:0},a=[];function i(e){if(n[e])return n[e].exports;var o=n[e]={i:e,l:!1,exports:{}};return t[e].call(o.exports,o,o.exports,i),o.l=!0,o.exports}i.m=t,i.c=n,i.d=function(t,e,o){i.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:o})},i.r=function(t){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},i.t=function(t,e){if(1&e&&(t=i(t)),8&e)return t;if(4&e&&"object"===typeof t&&t&&t.__esModule)return t;var o=Object.create(null);if(i.r(o),Object.defineProperty(o,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var n in t)i.d(o,n,function(e){return t[e]}.bind(null,n));return o},i.n=function(t){var e=t&&t.__esModule?function(){return t["default"]}:function(){return t};return i.d(e,"a",e),e},i.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},i.p="/";var s=window["webpackJsonp"]=window["webpackJsonp"]||[],c=s.push.bind(s);s.push=e,s=s.slice();for(var l=0;l<s.length;l++)e(s[l]);var u=c;a.push([0,"chunk-vendors"]),o()})({0:function(t,e,o){t.exports=o("56d7")},"034f":function(t,e,o){"use strict";var n=o("64a9"),r=o.n(n);r.a},"100b":function(t,e,o){"use strict";var n=o("7fec"),r=o.n(n);r.a},"34c8":function(t,e,o){},"3ffd":function(t,e,o){"use strict";var n=o("34c8"),r=o.n(n);r.a},"56d7":function(t,e,o){"use strict";o.r(e);o("cadf"),o("551c"),o("f751"),o("097d");var n=o("2b0e"),r=o("7591"),a=o.n(r),i=o("ecee"),s=o("c074"),c=o("ad3d"),l=function(){var t=this,e=t.$createElement,o=t._self._c||e;return o("div",{attrs:{id:"app"}},[t.connected?o("Canvas",{attrs:{username:t.username},on:{exit:t.exit}}):o("Home",{on:{start:function(e){return t.start(e)}}})],1)},u=[],f=(o("96cf"),o("3b8d")),h=o("1a9a"),d=function(){var t=this,e=t.$createElement,o=t._self._c||e;return o("div",{staticClass:"container"},[o("h1",{},[t._v("\n    Bienvenue sur le WhiteBoard Collaboratif !\n  ")]),o("form",{on:{submit:function(e){return e.preventDefault(),t.submit(e)}}},[o("label",{attrs:{for:"name"}},[t._v("Entrez votre surnom:")]),o("input",{directives:[{name:"model",rawName:"v-model",value:t.username,expression:"username"}],class:{invalid:t.invalidInput},attrs:{id:"name",autocomplete:"off",placeholder:"Surnom..."},domProps:{value:t.username},on:{focus:function(e){t.invalidInput=!1},input:function(e){e.target.composing||(t.username=e.target.value)}}}),o("button",[t._v("Commencer")])])])},p=[],m={name:"Home",data:function(){return{username:"",invalidInput:!1}},methods:{submit:function(){var t=this;if(!/\w/gim.test(this.username))return this.invalidInput=!1,void setTimeout((function(){t.invalidInput=!0}),50);console.log("Your name is ".concat(this.username)),this.$emit("start",this.username)}}},v=m,g=(o("5caf"),o("2877")),w=Object(g["a"])(v,d,p,!1,null,"6a694a76",null),b=w.exports,k=function(){var t=this,e=t.$createElement,o=t._self._c||e;return o("div",{ref:"container",staticClass:"container"},[o("tool-box",{ref:"toolbox",attrs:{tool:t.tool,params:t.toolParams},on:{"select-tool":function(e){return t.setTool(e)},"update-params":function(e){return Object.assign(t.toolParams,e)}}}),o("v-stage",{attrs:{config:t.configKonva},on:{mousedown:function(e){return t.startDrawing(e)},mousemove:function(e){return t.draw(e)},mouseup:function(e){return t.stopDrawing(e)}}},[o("v-layer",[t._l(t.shapes,(function(e,n){return[o(e.component,{key:n+"_"+t.tools[e.toolName].getKey(e),tag:"component",attrs:{config:Object.assign({},e.config,{draggable:"select"===t.tool})},on:{dragend:function(e){return t.handleDragEnd(e,n)}}})]}))],2)],1)],1)},y=[],x=(o("ac6a"),o("456d"),o("6762"),o("2fdb"),function(){var t=this,e=t.$createElement,o=t._self._c||e;return o("div",{attrs:{id:"toolbox"},on:{click:t.closeTools}},[o("button",{class:{active:"select"===t.tool},on:{click:function(e){return t.setTool("select")}}},[o("font-awesome-icon",{attrs:{icon:"mouse-pointer"}})],1),o("button",{class:{active:"freeLine"===t.tool},on:{click:function(e){return t.setTool("freeLine")}}},[o("font-awesome-icon",{attrs:{icon:"pen"}})],1),o("button",{class:{active:"circle"===t.tool},on:{click:function(e){return t.setTool("circle")}}},[o("font-awesome-icon",{attrs:{icon:"circle"}})],1),o("button",{staticClass:"colors-container",class:{active:"text"===t.tool},on:{click:function(e){e.stopPropagation(),t.setTool("text"),t.showText=!t.showText}}},[o("font-awesome-icon",{attrs:{icon:"font"}}),t.showText?o("div",{staticClass:"stroke-width",on:{click:function(t){t.stopPropagation()}}},[o("input",{directives:[{name:"model",rawName:"v-model",value:t.text,expression:"text"}],attrs:{id:"text",autocomplete:"off",placeholder:"Ton texte ici"},domProps:{value:t.text},on:{input:[function(e){e.target.composing||(t.text=e.target.value)},function(e){return t.updateParam({text:t.text})}]}})]):t._e()],1),o("div",{staticClass:"divider"}),o("button",{staticClass:"colors-container",style:{color:t.color},on:{click:function(e){e.stopPropagation(),t.showColors=!t.showColors}}},[o("font-awesome-icon",{attrs:{icon:"palette"}}),o("div",{directives:[{name:"show",rawName:"v-show",value:t.showColors,expression:"showColors"}],staticClass:"colors",on:{click:function(t){t.stopPropagation()}}},t._l(t.colors,(function(e){return o("button",{key:e,style:{color:e,backgroundColor:e},on:{click:function(o){return t.updateParam({color:e})}}})})),0)],1),o("button",{staticClass:"colors-container",on:{click:function(e){e.stopPropagation(),t.showStroke=!t.showStroke}}},[o("span",{staticStyle:{"font-size":"0.6rem"}},[o("font-awesome-icon",{attrs:{icon:"circle"}})],1),o("span",{staticStyle:{"font-size":"1rem"}},[o("font-awesome-icon",{attrs:{icon:"circle"}})],1),o("span",[o("font-awesome-icon",{attrs:{icon:"circle"}})],1),o("span",{staticStyle:{"font-size":"1rem","margin-left":"0.2rem"}},[t._v(t._s(t.strokeWidth)+"px")]),t.showStroke?o("div",{staticClass:"stroke-width",on:{click:function(t){t.stopPropagation()}}},[o("input",{directives:[{name:"model",rawName:"v-model",value:t.strokeWidth,expression:"strokeWidth"}],staticClass:"slider",attrs:{type:"range",min:"3",max:"100",value:"15",id:"myRange"},domProps:{value:t.strokeWidth},on:{change:function(e){t.updateParam({strokeWidth:parseInt(t.strokeWidth,10)})},__r:function(e){t.strokeWidth=e.target.value}}})]):t._e()])])}),O=[],j={name:"Toolbox",props:{tool:{type:String,default:""},params:{type:Object,default:function(){}}},data:function(){return{strokeWidth:15,color:"rgb(76, 76, 76)",showColors:!1,showStroke:!1,showText:!1,colors:["black","red","blue","green","yellow","white"],text:""}},methods:{updateParam:function(t){t.color&&(this.color=t.color,this.showColors=!1),this.$emit("update-params",t)},setTool:function(t,e){this.$emit("select-tool",{tool:t,params:e||{}})},closeTools:function(){this.showColors=!1,this.showStroke=!1,this.showText=!1}}},P=j,C=(o("3ffd"),Object(g["a"])(P,x,O,!1,null,"3f5b4a40",null)),_=C.exports,T=o("d225"),D=o("b0b4"),W=o("308d"),S=o("6bb5"),N=o("4e2b"),E=function(){function t(){if(Object(T["a"])(this,t),this.constructor===t)throw new TypeError('Abstract class "Shape" cannot be instantiated directly');this.shapeName=""}return Object(D["a"])(t,[{key:"startDrawing",value:function(t){throw new Error("You must implement this function")}},{key:"draw",value:function(t){throw new Error("You must implement this function")}},{key:"stopDrawing",value:function(t){throw new Error("You must implement this function")}},{key:"getKey",value:function(t){throw new Error("You must implement this function")}}]),t}(),$=E,Y=function(t){function e(){var t;return Object(T["a"])(this,e),t=Object(W["a"])(this,Object(S["a"])(e).call(this)),t.shapeName="v-line",t.defaultParams={color:"black",strokeWidth:10},t}return Object(N["a"])(e,t),Object(D["a"])(e,[{key:"startDrawing",value:function(t,e){return t&&t.evt?{component:this.shapeName,toolName:"freeLine",config:{points:[t.evt.offsetX,t.evt.offsetY,t.evt.offsetX,t.evt.offsetY],stroke:e.color||this.defaultParams.color,strokeWidth:e.strokeWidth||this.defaultParams.strokeWidth,lineCap:"round",lineJoin:"round"}}:null}},{key:"draw",value:function(t,e){t&&t.evt&&e.config.points.push(t.evt.offsetX,t.evt.offsetY)}},{key:"stopDrawing",value:function(t,e){}},{key:"getKey",value:function(t){return t.config.points.length}}]),e}($),K=Y,I=function(t){function e(){var t;return Object(T["a"])(this,e),t=Object(W["a"])(this,Object(S["a"])(e).call(this)),t.shapeName="v-circle",t.defaultParams={color:"black",strokeWidth:10},t}return Object(N["a"])(e,t),Object(D["a"])(e,[{key:"startDrawing",value:function(t,e){if(!t||!t.evt)return null;var o=e.strokeWidth||this.defaultParams.strokeWidth;return{component:this.shapeName,toolName:"circle",config:{x:t.evt.offsetX,y:t.evt.offsetY,radius:o/2,minRadius:o/2,stroke:e.color||this.defaultParams.color,strokeWidth:o,fillEnabled:!1,lineCap:"round",lineJoin:"round"}}}},{key:"draw",value:function(t,e){if(t&&t.evt){var o=t.evt.offsetX-e.config.x,n=t.evt.offsetY-e.config.y;e.config.radius=Math.max(Math.sqrt(o*o+n*n),e.config.minRadius)}}},{key:"stopDrawing",value:function(t,e){}},{key:"getKey",value:function(t){return t.config.radius}}]),e}($),M=I,X=function(t){function e(){var t;return Object(T["a"])(this,e),t=Object(W["a"])(this,Object(S["a"])(e).call(this)),t.shapeName="v-text",t.defaultParams={color:"black",strokeWidth:10,text:""},t}return Object(N["a"])(e,t),Object(D["a"])(e,[{key:"startDrawing",value:function(t,e){return t&&t.evt?{component:this.shapeName,toolName:"text",config:{x:t.evt.offsetX,y:t.evt.offsetY,text:e.text||this.defaultParams.text,fontSize:e.strokeWidth||this.defaultParams.strokeWidth,fontFamily:"Calibri",fill:e.color||this.defaultParams.color}}:null}},{key:"draw",value:function(t,e){}},{key:"stopDrawing",value:function(t,e){}},{key:"getKey",value:function(t){return t.config.text.length}}]),e}($),z=X,R={name:"Canvas",components:{ToolBox:_},props:{username:{type:String,default:""}},mounted:function(){this.configKonva.width=this.$refs.container.clientWidth,this.configKonva.height=this.$refs.container.clientHeight-51},data:function(){return{configKonva:{width:0,height:0},shapes:[],isDrawing:!1,toolParams:{},tool:"select",tools:{freeLine:new K,circle:new M,text:new z}}},methods:{startDrawing:function(t){if(this.$refs.toolbox&&this.$refs.toolbox.closeTools(),Object.keys(this.tools).includes(this.tool)){var e=this.tools[this.tool].startDrawing(t,this.toolParams);null!==e&&(this.shapes.push(e),this.isDrawing=!0)}},draw:function(t){if(this.isDrawing){var e=this.shapes[this.shapes.length-1]||null;if(null===e)return;this.tools[this.tool].draw(t,e)}},stopDrawing:function(t){if(this.isDrawing){var e=this.shapes[this.shapes.length-1]||null;this.tools[this.tool].stopDrawing(t,e),this.isDrawing=!1}},setTool:function(t){var e=t.tool,o=t.params;this.tool=e,this.toolParams=Object.assign(this.toolParams,o)},handleDragEnd:function(t,e){console.log("Forme n'".concat(e," :")),console.log(t.target.attrs)}}},H=R,J=(o("100b"),Object(g["a"])(H,k,y,!1,null,"3c97b654",null)),L=J.exports,B={name:"app",components:{Home:b,Canvas:L},data:function(){return{connected:!1,username:""}},methods:{start:function(){var t=Object(f["a"])(regeneratorRuntime.mark((function t(e){var o;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return this.username=e,t.prev=1,o=(new h["a"]).withUrl("ws-client").build(),t.next=5,o.start();case 5:console.log("Connected !"),t.next=11;break;case 8:t.prev=8,t.t0=t["catch"](1),console.log(t.t0);case 11:this.connected=!0;case 12:case"end":return t.stop()}}),t,this,[[1,8]])})));function e(e){return t.apply(this,arguments)}return e}(),exit:function(){this.username="",this.connected=!1}}},F=B,q=(o("034f"),Object(g["a"])(F,l,u,!1,null,null,null)),A=q.exports;n["default"].config.productionTip=!1,n["default"].use(a.a),i["c"].add(s["c"]),i["c"].add(s["e"]),i["c"].add(s["a"]),i["c"].add(s["d"]),i["c"].add(s["f"]),i["c"].add(s["b"]),n["default"].component("font-awesome-icon",c["a"]),new n["default"]({render:function(t){return t(A)}}).$mount("#app")},"5caf":function(t,e,o){"use strict";var n=o("c1fc"),r=o.n(n);r.a},"64a9":function(t,e,o){},"7fec":function(t,e,o){},c1fc:function(t,e,o){}});
//# sourceMappingURL=app.c9c50758.js.map