(window.vcvWebpackJsonp4x=window.vcvWebpackJsonp4x||[]).push([[0],{"./featureSection/index.js":function(e,t,n){"use strict";n.r(t);var o=n("./node_modules/vc-cake/index.js"),i=n.n(o),a=n("./node_modules/@babel/runtime/helpers/extends.js"),c=n.n(a),r=n("./node_modules/@babel/runtime/helpers/classCallCheck.js"),s=n.n(r),l=n("./node_modules/@babel/runtime/helpers/createClass.js"),u=n.n(l),d=n("./node_modules/@babel/runtime/helpers/possibleConstructorReturn.js"),p=n.n(d),g=n("./node_modules/@babel/runtime/helpers/getPrototypeOf.js"),m=n.n(g),b=n("./node_modules/@babel/runtime/helpers/inherits.js"),f=n.n(b),v=n("./node_modules/react/index.js"),x=n.n(v),k=Object(o.getService)("api"),h=Object(o.getService)("cook"),y=function(e){function t(){return s()(this,t),p()(this,m()(t).apply(this,arguments))}return f()(t,e),u()(t,[{key:"render",value:function(){var e=this.props,t=e.id,o=e.atts,i=e.editor,a=o.description,r=o.image,s=o.imageAlignment,l=o.reverseStacking,u=o.addButton,d=o.customClass,p=o.button,g=o.metaCustomId,m=n("./node_modules/classnames/index.js"),b={},f=m({"vce-feature-section-container":!0,"vce-feature-section-media--xs":!0}),v=m({vce:!0,"vce-feature-section":!0,"vce-feature-section--min-height":!0,"vce-feature-section--reverse":l}),k=["vce-feature-section-image"],y=["vce-feature-section-content"];"string"==typeof d&&d&&(v+=" ".concat(d));var C={};r&&(C.backgroundImage="url(".concat(this.getImageUrl(r),")")),s&&k.push("vce-feature-section-image--alignment-".concat(s));var j=this.getMixinData("backgroundColor");j&&y.push("vce-feature-section-background-color--".concat(j.selector)),(j=this.getMixinData("backgroundPosition"))&&k.push("vce-feature-section-image--background-position-".concat(j.selector));var S="";u&&(S=h.get(p).render(null,!1));g&&(b.id=g),r&&r.filter&&"normal"!==r.filter&&k.push("vce-image-filter--".concat(r.filter)),y=m(y),k=m(k);var w=this.applyDO("padding"),_=this.applyDO("margin background border animation");return x.a.createElement("section",c()({className:f},i,b),x.a.createElement("div",c()({className:v,id:"el-"+t},_),x.a.createElement("div",{className:k,style:C}),x.a.createElement("div",{className:y},x.a.createElement("div",c()({className:"vce-feature-section-content-container"},w),x.a.createElement("div",{className:"vce-feature-section-description"},a),S))))}}]),t}(k.elementComponent);(0,i.a.getService("cook").add)(n("./featureSection/settings.json"),function(e){e.add(y)},{css:n("./node_modules/raw-loader/index.js!./featureSection/styles.css"),editorCss:!1,mixins:{backgroundColor:{mixin:n("./node_modules/raw-loader/index.js!./featureSection/cssMixins/backgroundColor.pcss")},backgroundPosition:{mixin:n("./node_modules/raw-loader/index.js!./featureSection/cssMixins/backgroundPosition.pcss")}}},"")},"./featureSection/settings.json":function(e){e.exports={tag:{access:"protected",type:"string",value:"featureSection"},description:{type:"htmleditor",access:"public",value:"<h1>Beyond Beach and Cloudy Waves</h1>\n<p>A beach is a landform along the coast of an ocean or sea, or the edge of a lake or river. Beaches typically occur in areas along the coast where wave or current action deposits and reworks sediments.</p>",options:{label:"Description",inline:!0,dynamicField:!0,skinToggle:"darkTextSkin"}},darkTextSkin:{type:"toggleSmall",access:"public",value:!1},backgroundColor:{type:"color",access:"public",value:"#b3a694",options:{label:"Background color",cssMixin:{mixin:"backgroundColor",property:"backgroundColor",namePattern:"[\\da-f]+"}}},image:{type:"attachimage",access:"public",value:"feature-section-background.jpg",options:{label:"Image",multiple:!1,dynamicField:!0,defaultValue:"feature-section-background.jpg",imageFilter:!0}},backgroundImagePosition:{type:"buttonGroup",access:"public",value:"center center",options:{label:"Image position",cssMixin:{mixin:"backgroundPosition",property:"backgroundPosition",namePattern:"[a-z]+"},values:[{label:"Left Top",value:"left top",icon:"vcv-ui-icon-attribute-background-position-left-top"},{label:"Center Top",value:"center top",icon:"vcv-ui-icon-attribute-background-position-center-top"},{label:"Right Top",value:"right top",icon:"vcv-ui-icon-attribute-background-position-right-top"},{label:"Left Center",value:"left center",icon:"vcv-ui-icon-attribute-background-position-left-center"},{label:"Center Center",value:"center center",icon:"vcv-ui-icon-attribute-background-position-center-center"},{label:"Right Center",value:"right center",icon:"vcv-ui-icon-attribute-background-position-right-center"},{label:"Left Bottom",value:"left bottom",icon:"vcv-ui-icon-attribute-background-position-left-bottom"},{label:"Center Bottom",value:"center bottom",icon:"vcv-ui-icon-attribute-background-position-center-bottom"},{label:"Right Bottom",value:"right bottom",icon:"vcv-ui-icon-attribute-background-position-right-bottom"}]}},imageAlignment:{type:"buttonGroup",access:"public",value:"left",options:{label:"Image alignment",values:[{label:"Left",value:"left",icon:"vcv-ui-icon-attribute-alignment-left"},{label:"Right",value:"right",icon:"vcv-ui-icon-attribute-alignment-right"}]}},reverseStacking:{type:"toggle",access:"public",value:!1,options:{label:"Reverse stacking"}},addButton:{type:"toggle",access:"public",value:!0,options:{label:"Add button"}},customClass:{type:"string",access:"public",value:"",options:{label:"Extra class name",description:"Add an extra class name to the element and refer to it from Custom CSS option."}},button:{type:"element",access:"public",value:{tag:"outlineButton",alignment:"left"},options:{category:"Button",tabLabel:"Button",merge:{attributes:[{key:"alignment",type:"string"},{key:"buttonText",type:"string"},{key:"buttonUrl",type:"object"}]},onChange:{rules:{addButton:{rule:"toggle"}},actions:[{action:"toggleSectionVisibility"}]}}},designOptions:{type:"designOptions",access:"public",value:{},options:{label:"Design Options"}},editFormTab1:{type:"group",access:"protected",value:["description","image","backgroundImagePosition","imageAlignment","backgroundColor","reverseStacking","addButton","metaCustomId","customClass"],options:{label:"General"}},metaEditFormTabs:{type:"group",access:"protected",value:["editFormTab1","button","designOptions"]},relatedTo:{type:"group",access:"protected",value:["General"]},metaCustomId:{type:"customId",access:"public",value:"",options:{label:"Element ID",description:"Apply unique ID to element to link directly to it by using #your_id (for element ID use lowercase input only)."}}}},"./node_modules/raw-loader/index.js!./featureSection/cssMixins/backgroundColor.pcss":function(e,t){e.exports=".vce-feature-section-background-color--$selector {\n  @if $backgroundColor != false {\n    background-color: $backgroundColor;\n  }\n}"},"./node_modules/raw-loader/index.js!./featureSection/cssMixins/backgroundPosition.pcss":function(e,t){e.exports=".vce-feature-section-image--background-position-$selector {\n  @if $backgroundPosition != false {\n    background-position: $backgroundPosition;\n  }\n}"},"./node_modules/raw-loader/index.js!./featureSection/styles.css":function(e,t){e.exports=".vce-feature-section {\n  display: -ms-flexbox;\n  display: flex;\n  -ms-flex-direction: column;\n      flex-direction: column;\n  -ms-flex-pack: center;\n      justify-content: center;\n  overflow: hidden;\n  box-sizing: border-box;\n  min-height: 450px;\n}\n.vce-feature-section--reverse {\n  -ms-flex-direction: column-reverse;\n      flex-direction: column-reverse;\n}\n.vce-feature-section-image {\n  -ms-flex: 1 0 300px;\n      flex: 1 0 300px;\n  background-color: #b3a694;\n  background-repeat: no-repeat;\n  background-size: cover;\n  background-position: center;\n}\n.vce-feature-section-image--alignment-left {\n  -ms-flex-order: 1;\n      order: 1;\n}\n.vce-feature-section-image--alignment-right {\n  -ms-flex-order: 3;\n      order: 3;\n}\n.vce-feature-section-content {\n  -ms-flex: 1 0 auto;\n      flex: 1 0 auto;\n  -ms-flex-order: 2;\n      order: 2;\n  background-color: #b3a694;\n  background-position: center;\n  padding: 30px 0;\n  color: #fff;\n}\n.vce-feature-section-content h1 {\n  color: inherit;\n}\n.vce-feature-section-content-container {\n  padding: 30px;\n}\n.vce-feature-section .vce {\n  margin-bottom: 0;\n}\n@media (min-width: 768px) {\n  .vce-feature-section {\n    -ms-flex-direction: row;\n        flex-direction: row;\n  }\n  .vce-feature-section-image {\n    -ms-flex: 1 1 50%;\n        flex: 1 1 50%;\n  }\n  .vce-feature-section-content {\n    display: -ms-flexbox;\n    display: flex;\n    -ms-flex: 1 1 50%;\n        flex: 1 1 50%;\n    -ms-flex-direction: column;\n        flex-direction: column;\n    -ms-flex-pack: center;\n        justify-content: center;\n    -ms-flex-align: start;\n        align-items: flex-start;\n  }\n  .vce-feature-section-content-container {\n    padding: 0;\n    max-width: 585px;\n    width: 66.66666667%;\n    margin: 0 auto;\n  }\n}\n"}},[["./featureSection/index.js"]]]);