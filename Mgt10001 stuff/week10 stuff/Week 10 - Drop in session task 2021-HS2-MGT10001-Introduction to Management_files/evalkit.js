var evalkit_loaded=0,evalkit_issafari=-1!==navigator.userAgent.indexOf("Safari")&&(-1===navigator.userAgent.indexOf("Chrome")||-1<navigator.userAgent.indexOf("Chrome")&&-1<navigator.userAgent.indexOf("OPR")),EvaluationKIT={onLoad:function(){if(!(null===ENV.current_user_id||-1<window.location.href.indexOf("sessionless_launch")||0===$("#dashboard").length&&0===$(".context_external_tool_"+evalkit_setup.auth_external_tool_id).length&&0===this.getPageCourseId()&&-1===document.location.href.indexOf("/profile")&&-1===document.location.href.indexOf("/grades")||3<(evalkit_loaded+=1))){var e=this.getTokenCookie(),t=this.getVerifyCookie();0===e.length||t!==ENV.current_user_id?this.onLti():this.onUser(e)}},onLti:function(){this.startInit(function(){(EvaluationKIT||{}).onAuth()})},startInit:function(o){var n=this,e=n.getInitCookie();null!==e&&!1===e||(null===e||!0!==e?$.get("/api/v1/users/self/profile",function(t){if(0<t.login_id.length){var e=n.getPageCourseId();0<e?$.get("/api/v1/courses/"+e,function(e){0<e.id?n.onAuthInit(t.id,t.login_id,e.id,e.course_code,o):n.setInitCookie(!1)}):n.onAuthInit(t.id,t.login_id,0,"",o)}else n.setInitCookie(!1)}):o())},onAuthInit:function(e,t,o,n,i){var a=this;$.get(evalkit_setup.account_url+"/Canvas/InitCheck?authId="+evalkit_setup.auth_external_tool_id+"&username="+encodeURIComponent(t)+"&userid="+e+"&coursecode="+encodeURIComponent(n)+"&courseid="+o+"&refer="+encodeURIComponent(window.location.pathname+window.location.search)+"&nocache="+(new Date).getTime(),function(e){a.setInitCookie(e.result),!1!==e.result&&i()})},onAuth:function(){var t=this.createCORSRequest("GET","/api/v1/accounts/"+evalkit_setup.account_id+"/external_tools/sessionless_launch?launch_type=user_navigation&url="+encodeURIComponent(evalkit_setup.service_url+"/canvas/authlti"));t&&(t.onload=function(){if(200===t.status){var e=$.parseJSON(t.responseText.replace("while(1);",""));0<$("#evalkit-lti").length?$("#evalkit-lti").attr("src",e.url):$('<iframe src="'+e.url+'" id="evalkit-lti" style="display:none;"></iframe>').appendTo($("body"))}else{(EvaluationKIT||{}).onLoad()}},t.send())},onLtiResponse:function(e){if(null!==e.origin&&(-1!==e.origin.toLowerCase().indexOf(evalkit_setup.service_url)||-1!==e.origin.toLowerCase().indexOf(evalkit_setup.account_url))&&null!==e.data&&void 0!==e.data.length){var t=EvaluationKIT||{},o=JSON.parse(e.data);if(null!==o)switch(o.subject){case"evalkit.location":window.top.location.href=o.url;break;case"evalkit.ltiresponse":var n=o.token;0===n.length&&(n="-1"),t.setTokenCookie(n),t.setVerifyCookie(ENV.current_user_id),t.onLoad()}else console.log("DEBUG: onLtiResponse - jsondata error")}},onUser:function(a){"-1"!==a&&this.startInit(function(){var e=EvaluationKIT||{},t=e.getPageCourseId();if(null===document.getElementById("ek_css")){var o=document.createElement("link");o.setAttribute("id","ek_css"),o.setAttribute("rel","stylesheet"),o.setAttribute("type","text/css"),o.setAttribute("href",evalkit_setup.account_url+"/scripts/canvas/style.min.css"),document.getElementsByTagName("head")[0].appendChild(o)}if(0===t)e.getUserSettings(a,t,"");else{var n="/api/v1/courses/"+t,i=e.createCORSRequest("GET",n);if(!i)return;i.onload=function(){var e="";try{e=$.parseJSON(i.responseText).course_code}catch(e){newobjerrnfo.onError(e)}(EvaluationKIT||{}).getUserSettings(a,t,e)},i.onerror=function(e){var t=EvaluationKIT||{};t.onError(e),t.onLoad()},i.send()}})},getUserSettings:function(e,o,t){var n=this.createCORSRequest("GET",evalkit_setup.service_url+"/canvas/usersettings?token="+encodeURIComponent(e)+"&userid="+ENV.current_user_id+(0<o?"&courseid="+o+"&coursecode="+encodeURIComponent(t)+"&courseTitle=":"")+"&refer="+encodeURIComponent(window.location.pathname+window.location.search)+"&nocache="+(new Date).getTime());n&&(n.onload=function(){if(401===n.status)(EvaluationKIT||{}).onLti();else try{var e=$.parseJSON(n.responseText),t=EvaluationKIT||{};t.onPopup(e),t.onWidget(e),t.onLinks(e,o),$(document).on("click",".ek-sessionless",function(e){var t=$(this).attr("href");evalkit_issafari?t=$(this).data("safariurl"):e.preventDefault();var o=(EvaluationKIT||{}).createCORSRequest("GET",t);o&&(o.onload=function(){if(200===o.status){var e=$.parseJSON(o.responseText.replace("while(1);",""));if(void 0!==e.url&&null!==e&&0<e.url.length)ek_modal.open({iframelink:e.url,buttons:null,iframeheight:800,cssclass:"evalkit-modal-lg",showClose:!0,type:"notification"});else(EvaluationKIT||{}).onLti()}else{(EvaluationKIT||{}).onLti()}},o.send())})}catch(e){(EvaluationKIT||{}).onError(e)}},n.onerror=function(e){var t=EvaluationKIT||{};-1<navigator.appVersion.indexOf("MSIE 10")||-1<navigator.appVersion.indexOf("MSIE 9")?t.onLti():t.onError(e)},n.send())},onPopup:function(o){if(!0===o.popup.visible){var e=[],t=document.createElement("a");if(t.innerHTML=o.popup.gotosurveytext,-1<o.popup.gotosurveyurl.indexOf("sessionless_launch")?(t.setAttribute("class","Button Button--primary ek-widget-btn-primary ek-sessionless"),evalkit_issafari?(t.setAttribute("href",evalkit_setup.account_url+"/MyEval/CookieSet.aspx"),t.setAttribute("data-safariurl",o.popup.gotosurveyurl),t.setAttribute("target","_blank")):t.setAttribute("href",o.popup.gotosurveyurl)):(evalkit_issafari&&(o.popup.gotosurveyurl=evalkit_setup.account_url+"/MyEval/SafariRedirect.aspx?ret="+encodeURIComponent(window.location.origin+o.popup.gotosurveyurl)),t.setAttribute("class","Button Button--primary ek-widget-btn-primary"),t.setAttribute("href",o.popup.gotosurveyurl)),e[0]=t,o.popup.remindlater){var n=document.createElement("a");n.innerHTML=o.popup.remindlatertext,n.setAttribute("href","#"),n.setAttribute("class","Button ek-widget-btn-default"),!0===o.popup.incrementDefer?n.onclick=function(e){e.preventDefault(),ek_modal.close();var t=EvaluationKIT||{};$.get(evalkit_setup.service_url+"/canvas/Defer?token="+encodeURIComponent(t.getTokenCookie())+"&projectId="+o.popup.projectId+"&courseId="+o.popup.courseId+"&nocache="+(new Date).getTime())}:n.onclick=function(e){e.preventDefault(),ek_modal.close()},e[1]=n}ek_modal.open({title:o.popup.header,body:o.popup.body,buttons:e,showClose:!1,type:!0===o.popup.blockPage?"blocker":"notification"})}},onWidget:function(e){$(".ek-widget").remove(),$(e.widget).appendTo("#right-side"),evalkit_issafari&&null!=e.widget&&$(".ek-widget a").each(function(e){var t=$(this);t.hasClass("ek-sessionless")?(t.attr("data-safariurl",t.attr("href")),t.attr("href",evalkit_setup.account_url+"/MyEval/CookieSet.aspx"),t.attr("target","_blank")):t.attr("href",evalkit_setup.account_url+"/MyEval/SafariRedirect.aspx?ret="+encodeURIComponent(window.location.origin+$(this).attr("href")))})},onLinks:function(e,t){$("a.context_external_tool_"+evalkit_setup.auth_external_tool_id).remove(),this.setLink(e.userlink,t),this.setLink(e.studentlink,t),this.setLink(e.instructorlink,t),this.setLink(e.talink,t),this.setLink(e.adminlink,t)},createCORSRequest:function(e,t){try{var o=new XMLHttpRequest;return"withCredentials"in o?o.open(e,t,!0):"undefined"!=typeof XDomainRequest?(o=new XDomainRequest).open(e,t):o=null,o}catch(e){this.onError(e)}},getPageCourseId:function(){return void 0!==$("body").attr("class")&&$("body").attr("class").match(/\bcontext-course_(.[0-9]*)/)?parseInt($("body").attr("class").match(/\bcontext-course_(.[0-9]*)/)[1]):0},getPageUserId:function(){return $("body").attr("class").match(/\bcontext-user_(.[0-9]*)/)?$("body").attr("class").match(/\bcontext-user_(.[0-9]*)/)[1]:""},getTokenCookie:function(){return evalkit_readCookie("evalkit_token_"+evalkit_setup.auth_external_tool_id)||""},setTokenCookie:function(e){evalkit_createCookie("evalkit_token_"+evalkit_setup.auth_external_tool_id,e)},getVerifyCookie:function(){return evalkit_readCookie("evalkit_verify_"+evalkit_setup.auth_external_tool_id)||""},setVerifyCookie:function(e){evalkit_createCookie("evalkit_verify_"+evalkit_setup.auth_external_tool_id,e)},getInitCookie:function(){var e=evalkit_readCookie("evalkit_init_verfiy_"+evalkit_setup.auth_external_tool_id);if(null!==e&&""!==e||(e=-1),-1!==e&&e!==ENV.current_user_id)return evalkit_createCookie("evalkit_init_verfiy_"+evalkit_setup.auth_external_tool_id,ENV.current_user_id),evalkit_createCookie("evalkit_init_"+evalkit_setup.auth_external_tool_id,""),null;var t=evalkit_readCookie("evalkit_init_"+evalkit_setup.auth_external_tool_id);if(null===t||""===t)return null;var o=this.getSection();if(null===o)return!1;for(var n=JSON.parse(t),i=0;i<n.length;i++)if(n[i].section==o)return n[i].val;return null},setInitCookie:function(e){var t=evalkit_readCookie("evalkit_init_"+evalkit_setup.auth_external_tool_id);null!==t&&""!==t||(t="");var o=this.getSection();if(null!==o){for(var n=0===t.length?[]:JSON.parse(t),i=!1,a=0;a<n.length;a++)n[a].section==o&&(i=!0,n[a].val=e);!1===i&&n.push({section:o,val:e}),evalkit_createCookie("evalkit_init_verfiy_"+evalkit_setup.auth_external_tool_id,ENV.current_user_id),evalkit_createCookie("evalkit_init_"+evalkit_setup.auth_external_tool_id,JSON.stringify(n))}},getSection:function(){var e=null;return 0<$("#dashboard").length?e="dashboard":-1<document.location.href.indexOf("/profile")?e="profile":0<this.getPageCourseId()?e=this.getPageCourseId():-1<document.location.href.indexOf("/grades")&&(e="dashboard"),e},setLink:function(e,t){if(null!==e&&0!==e.exttoolid){var o=$("a.context_external_tool_"+e.exttoolid);if(2!=e.linktype||0!==this.getPageUserId().length){if(2==e.linktype&&0<this.getPageUserId().length){if(!e.visible)return void(o.hasClass("evalkit-ltilink")||o.parent().hide());0===o.length?($("<li class='section evalkitlink'><a style='display:block !important;' class='evalkituser context_external_tool_"+e.exttoolid+"' href='/users/"+ENV.current_user_id+"/external_tools/"+e.exttoolid+"'>"+e.title+"</a></li>").appendTo("#section-tabs"),o=$("a.context_external_tool_"+e.exttoolid)):(e.title!==o.html()&&o.html(e.title),o.attr("style","display: block !important;")),o.parent().show()}else if(0<t){if(!e.visible)return void(o.hasClass("evalkit-ltilink")||o.parent().hide());0===o.length?($("<li class='section evalkitlink'><a style='display:block !important;' class='evalkitcourse context_external_tool_"+e.exttoolid+"' href='/courses/"+t+"/external_tools/"+e.exttoolid+"'>"+e.title+"</a></li>").appendTo("#section-tabs"),o=$("a.context_external_tool_"+e.exttoolid)):(o.html(e.title),o.attr("style","display: block !important;")),o.parent().show()}o.addClass("evalkit-ltilink"),0<o.length&&(null!==e.badge&&$('<b class="nav-badge evalkit-badge">'+e.badge+"</b>").prependTo(o),evalkit_issafari&&o.attr("href",evalkit_setup.account_url+"/MyEval/SafariRedirect.aspx?ret="+encodeURIComponent(window.location.origin+o.attr("href"))))}}},onError:function(e){console.log("EvaluationKIT Canvas User Integration Error: "+e),console.log("EvaluationKIT Canvas User Integration Error Stack: "+e.stack)}};$(document).ready(function(){var e=EvaluationKIT||{};window.addEventListener?window.addEventListener("message",e.onLtiResponse,!1):window.attachEvent("onmessage",e.onLtiResponse),e.onLoad()});var ek_modal=function(){var n,i,a,l,r,s,e={};(n=document.createElement("div")).id="ek-overlay",n.style.display="none",(i=document.createElement("div")).id="ek-modal",i.style.display="none",i.setAttribute("role","dialog"),i.setAttribute("tabindex","-1"),i.setAttribute("aria-live","assertive"),i.setAttribute("aria-labelledby","ek-modal-header");var t=document.createElement("div");return t.setAttribute("class","ek-modal-content"),(a=document.createElement("h2")).setAttribute("class","ek-modal-header"),a.id="ek-modal-header",(s=document.createElement("a")).setAttribute("id","ek-modal-close"),(l=document.createElement("div")).setAttribute("class","ek-modal-body"),l.setAttribute("id","ek-modal-body"),(r=document.createElement("div")).setAttribute("class","ek-modal-footer"),t.appendChild(a),t.appendChild(s),t.appendChild(l),t.appendChild(r),i.appendChild(t),e.open=function(e){if(null!==e){if(void 0!==e.cssclass&&null!==e.cssclass&&i.setAttribute("class",e.cssclass),l.innerHTML="",void 0!==e.iframelink&&null!==e.iframelink&&0<e.iframelink.length?(l.innerHTML='<iframe id="ek-modal-iframe-loading"></iframe><iframe src='+e.iframelink+' id="ek-modal-iframe" style=\'display:none;\' onload="evalkit_modal_iframe();"></frame>',window.removeEventListener("resize",evalkit_modal_iframe_resize),window.addEventListener("resize",evalkit_modal_iframe_resize),evalkit_modal_iframe_resize(),document.getElementById("ek-modal-close").style.display="",document.getElementById("ek-modal-iframe-loading").contentWindow.document.write('<html><head></head><body style="text-align:center;"><img src="'+evalkit_setup.account_url+'/Media/Images/loadingd2l.gif" style="padding-top:10%;" /></body></html>')):l.innerHTML=e.body,void 0!==e.title&&null!==e.title?(a.innerHTML=e.title,a.style.display=""):a.style.display="none",n.setAttribute("class","ek-overlay-"+e.type),"blocker"==e.type){var t=window.getComputedStyle(document.getElementById("header"),null).getPropertyValue("background-color");0===t.length&&(t="rgb(57, 75, 88)"),n.style.background=t}else n.style.background="";if(i.setAttribute("oncontextmenu","return false"),n.setAttribute("oncontextmenu","return false"),n.style.display="",i.style.display="",i.setAttribute("data-type",e.type),window.scrollTo(0,0),$("body").addClass("ek-modal-open"),setTimeout(function(){$("body").removeClass("ek-modal-open")},2e3),void 0!==e.buttons&&null!==e.buttons&&0<e.buttons.length){r.innerHTML="";for(var o=0;o<e.buttons.length;o++)0===o&&e.buttons[o].focus(),r.appendChild(e.buttons[o]);r.style.display=""}else r.innerHTML="",r.style.display="none";!0!==e.showClose&&(s.style.display="none"),!0===e.refreshOnClose?s.onclick=function(e){$(document).keypress(function(e){if(27==e.keyCode)return e.preventDefault(),!1}),ek_modal.close(),document.location.href=document.location.href}:s.onclick=function(e){ek_modal.close()}}else console.log("ek_modalopen settings === null")},e.close=function(){$("body").removeClass("ek-modal-open"),n.style.display="none",i.style.display="none",l.innerHTML=""},document.body.appendChild(n),document.body.appendChild(i),e}();function evalkit_modal_iframe(e){var t=document.getElementById("ek-modal-iframe"),o=document.getElementById("ek-modal-iframe-loading");t.style.display="",o.style.display="none"}function evalkit_modal_iframe_resize(){var e=document.getElementById("ek-modal-iframe");null!==e&&e.setAttribute("height",window.innerHeight-180);var t=document.getElementById("ek-modal-iframe-loading");null!==t&&t.setAttribute("height",window.innerHeight-180)}function evalkit_createCookie(e,t){var o=new Date,n=Date.UTC(o.getFullYear(),o.getMonth(),o.getDate(),o.getHours(),o.getMinutes(),o.getSeconds(),o.getMilliseconds());sessionStorage.setItem(e+"_E",n),sessionStorage.setItem(e,t)}function evalkit_readCookie(e){var t=sessionStorage.getItem(e+"_E");if(null==t)return sessionStorage.removeItem(e),null;var o=new Date;return 18e5<Date.UTC(o.getFullYear(),o.getMonth(),o.getDate(),o.getHours(),o.getMinutes(),o.getSeconds(),o.getMilliseconds())-t?(sessionStorage.removeItem(e+"_E"),sessionStorage.removeItem(e),null):sessionStorage.getItem(e)}