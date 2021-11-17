/*pingdom START*/var pingdom_js = document.createElement('script');
pingdom_js.src = '//rum-static.pingdom.net/pa-5b5142daef13ce00160001bc.js';
document.head.appendChild(pingdom_js);/*pingdom END*/
/*EvaluationKIT START*/var evalkit_jshosted = document.createElement('script');
evalkit_jshosted.setAttribute('type', 'text/javascript');
evalkit_jshosted.setAttribute('src', 'https://swinburne.evaluationkit.com/CanvasScripts/swinburne.js?v=1');
document.getElementsByTagName('head')[0].appendChild(evalkit_jshosted);/*EvaluationKIT END*/
//CAN-1335 and CAN-1025
if (location.href.match(/https.*courses\/\d+\/settings.*/g) != null) {
    var buttonLinkArray = $('#right-side-wrapper').find('a');
    const subAccount = document.getElementById("course_account_id");
    const subAccountText = subAccount.selectedIndex === undefined ? subAccount.innerText.trim() : subAccount.options[subAccount.selectedIndex].text.trim();
    let isSandpit = subAccountText === "SANDPIT" || subAccountText === "SANDPITS";
    for (i = 0; i < buttonLinkArray.size(); i++) {
        if (buttonLinkArray[i].search.toLowerCase().indexOf('event=conclude') !== -1 || (buttonLinkArray[i].href.match(/https.*courses\/\d+\/reset.*/g) != null && !isSandpit)) {
            buttonLinkArray[i].style.display = 'none';
        }
    }
}
/*SearchTool START*/var query_string = window.location.href, query_string_split = query_string.split("/");
window.currentCourse = query_string_split.indexOf("courses") >= 0 ? query_string_split[query_string_split.indexOf("courses") + 1] : "", window.searchDialog, window.searchDialogId = "#searchDialog", window.searchFormId = "#searchForm", window.canvasSearch = {}, window.canvasSearch.allItems = [], window.canvasSearch.allItemsAjax = [], window.pages = [], window.searchList = [], window.courseIds = [], window.initCanvasSearch = function () {
    window.buildSearchDialog(), $("#searchToolbar").remove();
    $("body .ic-app-nav-toggle-and-crumbs").length > 0 && (window.searchToolbar = $("body .ic-app-nav-toggle-and-crumbs").append("     <div id='searchToolbar' class='' style='float: right; display: inline; text-align: right; font-size: 0.5em; line-height: 1em; '>       <a title='Search Canvas' onClick='window.openSearchDialog()' class='btn btn-default' style='padding: 0.5em;'><i class='icon-search'> </i></a>     </div>     "))
}, window.buildSearchDialog = function () {
    $(window.searchDialogId) > 0 && window.searchDialog.dialog("destroy"), $(window.searchDialogId).remove(), $("body").append('<div id="' + window.searchDialogId.substring(1) + '" title="Search Canvas Unit"></div>'), window.searchDialog = $(window.searchDialogId), window.searchDialog.attr("title", "Search Canvas Unit");
    var e = "   <div id='" + window.searchFormId.substring(1) + "'>";
    window.currentCourse ? e += "     <div class=''>       <h3>Unit ids <small>(multiple ids separated by a comma) (this unit is '" + window.currentCourse + "')</small></h3>       <p> <input id='searchCourseId' type='text' style='width: 90%;' value='" + window.currentCourse + "'> </p>     </div>" : e += "     <div class=''>       <h3>Unit ids <small>(multiple ids separated by a comma)</small></h3>       <p><input id='searchCourseId' type='text' style='width: 90%;' value=''> </p>     </div>", e += "     <div class=''>       <h3>Search terms <small>(each on a separate line)</small></h3>       <textarea id='searchTerms' style='width: 90%; height:200px;'>" + window.searchList.join("\n") + "</textarea>     </div>     <div id='searchOptions'>       <h3>Search type <small>         <label><input type='checkbox' checked='checked' value='Page'></input> Pages </label>         <label><input type='checkbox' checked='checked' value='Assignment'></input> Assignments </label>         <label><input type='checkbox' checked='checked' value='Discussion'></input> Discussions </label>         <label><input type='checkbox' checked='checked' value='Quiz'></input> Quizzes </label>         <label><input type='checkbox' checked='checked' value='Syllabus'></input> Syllabus </label>       </small> </h3>     </div>     <p><button id='searchStartButton' class='button btn' onclick='window.hideSearch(); window.runSearchDialog()'>Search</button></p>   </div>", e += "   <p><button id='showSearchButton' class='button btn' onclick='window.showSearch();'>Show search</button></p>   <h2 id='searchOverallResults'>Results<small></small></h2>   <div id='searchCourseResults'></div>", window.searchDialog.append(e), $("#showSearchButton", window.searchDialog).hide(), window.searchDialog.hide()
}, window.hideSearch = function () {
    $(window.searchFormId, window.searchDialog).hide("fast"), $("#showSearchButton", window.searchDialog).show("fast")
}, window.showSearch = function () {
    $(window.searchFormId, window.searchDialog).show("fast"), $("#showSearchButton", window.searchDialog).hide("fast")
}, window.openSearchDialog = function () {
    window.searchDialog.dialog(), window.searchDialog.dialog("option", "width", 1e3), window.searchDialog.dialog("option", "height", $(window).height()), window.searchDialog.dialog("option", "position", "top")
}, window.runSearchDialog = function () {
    window.clearSearchResults(), window.courseIds = [], window.searchList = [], $.each($("#searchCourseId").attr("value").split(","), function () {
        var e = this.trim();
        "" != e && window.courseIds.push(e)
    }), $.each($("#searchTerms").attr("value").split("\n"), function () {
        var e = this.trim();
        "" != e && window.searchList.push(e)
    });
    var e = [];
    $.each($('#searchOptions input[type="checkbox"]:checked'), function () {
        e.push($(this).attr("value"))
    }), e.length > 0 ? $.each(window.courseIds, function (a, t) {
        window.addSearchResultSection(t), window.canvasSearch.listAllItems(t, e)
    }) : $.each(window.courseIds, function (e, a) {
        window.addSearchResultSection(a), window.canvasSearch.addSearchError(a, "", "", "No Search types were selected.")
    })
}, window.clearSearchResults = function () {
    $(window.searchCourseResults).html("")
}, window.addSearchResultSection = function (e) {
    var a = "   <div id='resultSection" + e + "' class=''>     <h3 style='background: #EEE; cursor: pointer; padding: 5px 10px; border-radius: 10px; '>Results for course #" + e + "<small></small></h3>     <table class='table table-striped'>       <thead>         <tr>           <th>Page type</th>           <th>Search term</th>           <th width='20%'>Page title</th>           <th>Results</th>           <th>Link</th>         </tr>       </thead>       <tbody>       </tbody>     </table>     <div class='loading text-center'><img src='/images/ajax-reload-animated.gif'> Loading.</div>   </div>";
    return $(window.searchCourseResults).append($(a)), $("#resultSection" + e + " h3").on("click", function () {
        $(this).next("table").fadeToggle("fast")
    }), $("#resultSection" + e)
}, window.canvasSearch.listAllItems = function (e, a) {
    window.canvasSearch.allItems[e] = [], window.canvasSearch.allItemsAjax[e] = [];
    var t = [], s = {}, i = {};
    $.each(a, function (e, a) {
        t.push(a), s[a] = 1, i[a] = !1
    }), window.canvasSearch.listItemsFromCourse(e, t, s, i, window.canvasSearch.allItems[e], window.canvasSearch.allItemsAjax[e], window.canvasSearch.resultsListAllItems)
}, window.canvasSearch.listItemsFromCourse = function (e, a, t, s, i, n, r) {
    $.each(a, function (a, o) {
        var c = "", l = "", d = "";
        switch (o = o) {
            case"Page":
                c = "/api/v1/courses/" + e + "/pages?sort=title&page=" + t[o] + "&per_page=100", l = "/api/v1/courses/" + e + "/pages/", d = "url";
                break;
            case"Assignment":
                c = "/api/v1/courses/" + e + "/assignments?page=" + t[o] + "&per_page=100", l = "/api/v1/courses/" + e + "/assignments/", d = "id";
                break;
            case"Discussion":
                c = "/api/v1/courses/" + e + "/discussion_topics?page=" + t[o] + "&per_page=100", l = "/api/v1/courses/" + e + "/discussion_topics/", d = "id";
                break;
            case"Quiz":
                c = "/api/v1/courses/" + e + "/quizzes?page=" + t[o] + "&per_page=100", l = "/api/v1/courses/" + e + "/quizzes/", d = "id";
                break;
            case"Syllabus":
                c = "/api/v1/courses/" + e + "?include=syllabus_body", l = null, d = null
        }
        n.push($.ajax({type: "GET", url: c, success: function (a, c, h) {
                null == l ? (a.type = o, i.push(a), s[o] = !0) : ($.each(a, function (a, t) {
                    n.push($.ajax({type: "GET", url: l + t[d], success: function (e) {
                            e.type = o, i.push(e)
                        }, error: function (a, s, i) {
                            window.canvasSearch.listSearchError(e, a, s, o + " " + t[d] + " " + i)
                        }}))
                }), h.getResponseHeader("Link").indexOf('rel="next"') > 0 && t[o] < 100 ? (t[o]++, window.canvasSearch.listItemsFromCourse(e, [o], t, s, i, n, r)) : s[o] = !0);
                var u = !0;
                $.each(s, function (e, a) {
                    a || (u = !1)
                }), u && $.when.apply(void 0, n).then(function () {
                    r(e)
                })
            }, error: function (a, t, s) {
                window.canvasSearch.addSearchError(e, a, t, "Course " + e + " " + s)
            }}))
    })
}, window.canvasSearch.resultsListAllItems = function (e) {
    $(".loading", $("#resultSection" + e)).remove(), $.each(window.canvasSearch.allItems[e], function (a, t) {
        switch (t.type) {
            case"Page":
                $.each(window.searchList, function (a, s) {
                    if ((t.body + "").indexOf(s) > 0) {
                        var i = {type: "Page", searchTerm: s, pageId: t.page_id, pageTitle: t.title, resultContext: t.body.split("\n").filter(function (e) {
                                return e.indexOf(s) >= 0
                            }), url: t.html_url, data: t};
                        window.addToSearchResults(i, e)
                    }
                });
                break;
            case"Assignment":
                $.each(window.searchList, function (a, s) {
                    if ((t.description + "").indexOf(s) > 0) {
                        var i = {type: "Assignment", searchTerm: s, pageId: t.page_id, pageTitle: t.name, resultContext: t.description.split("\n").filter(function (e) {
                                return e.indexOf(s) >= 0
                            }), url: t.html_url, data: t};
                        window.addToSearchResults(i, e)
                    }
                });
                break;
            case"Discussion":
                $.each(window.searchList, function (a, s) {
                    if ((t.message + "").indexOf(s) > 0) {
                        var i = {type: "Discussion", searchTerm: s, pageId: t.page_id, pageTitle: t.title, resultContext: t.message.split("\n").filter(function (e) {
                                return e.indexOf(s) >= 0
                            }), url: t.html_url, data: t};
                        window.addToSearchResults(i, e)
                    }
                });
                break;
            case"Quiz":
                $.each(window.searchList, function (a, s) {
                    if ((t.description + "").indexOf(s) > 0) {
                        var i = {type: "Quiz", searchTerm: s, pageId: t.page_id, pageTitle: t.title, resultContext: t.description.split("\n").filter(function (e) {
                                return e.indexOf(s) >= 0
                            }), url: t.html_url, data: t};
                        window.addToSearchResults(i, e)
                    }
                });
                break;
            case"Syllabus":
                $.each(window.searchList, function (a, s) {
                    if (null != t.syllabus_body && (t.syllabus_body + "").indexOf(s) > 0) {
                        var i = {type: "Syllabus", searchTerm: s, pageId: "N/A", pageTitle: "Syllabus", resultContext: t.syllabus_body.split("\n").filter(function (e) {
                                return e.indexOf(s) >= 0
                            }), url: "/courses/" + e + "/assignments/syllabus", data: t};
                        window.addToSearchResults(i, e)
                    }
                })
        }
    }), 0 == $("table tbody tr", $("#resultSection" + e)).length && window.noSearchResults(e)
}, window.addToSearchResults = function (e, a) {
    for (var t = e.resultContext[0].split(e.searchTerm), s = 0; s < t.length; s++)
        t[s] = t[s].replace(/\</g, "&lt;").replace(/\>/g, "&gt;");
    e.resultContext = t.join("<span style='color:red;text-decoration:underline;'>" + e.searchTerm + "</span>");
    var i = "     <tr>       <td>" + e.type + "</td>       <td>" + e.searchTerm + "</td>       <td>" + e.pageTitle + "</td>       <td style='font-size: 0.85em; line-height: 1em;'>" + e.resultContext + "</td>       ";
    "" != e.url ? i += "<td><a href='" + e.url + "' target='_blank' class='button'><i class='icon-link'> </i></a></td>" : i += "<td>&nbsp;</td>", i += "</tr>", $("table tbody", $("#resultSection" + a)).append(i), $("#resultSection" + a).find("small").html("&nbsp;&nbsp;<i>" + $("#resultSection" + a + " table tbody > tr").length + " found.</i>"), $("#searchOverallResults").find("small").html("&nbsp;&nbsp;<i>" + $("#searchCourseResults table.table-striped tbody > tr").length + " found in all courses.</i>")
}, window.canvasSearch.addSearchError = function (e, a, t, s) {
    $(".loading", $("#resultSection" + e)).remove();
    var i = "   <tr>     <td>ERROR</td>     <td>" + t + "</td>     <td>" + s + "</td>     <td style='font-size: 0.85em; line-height: 1em;'>" + JSON.stringify(a) + "</td>     <td>&nbsp;</td>   </tr>   ";
    $("table tbody", $("#resultSection" + e)).append(i)
}, window.noSearchResults = function (e) {
    $("table tbody", $("#resultSection" + e)).append("   <tr>     <td>&nbsp;</td>     <td>&nbsp;</td>     <td><em>No results found.</em></td>     <td style='font-size: 0.85em; line-height: 1em;'>&nbsp;</td>     <td>&nbsp;</td>   </tr>   ")
}, ENV.current_user_roles.includes("admin") && window.initCanvasSearch();/*SearchTool END*/

/*CAN-1995 START -  */ 
// CAN-1995 : To insert AAS link to all staff irrespective they are part of any course (Note - No student will have AAS access or link in canvas)
function insertAASLink(){
	
	var currUserEmail = ENV.USER_EMAIL;
	var currUser = null;

	if (typeof(Storage) !== "undefined") {
		if(currUserEmail){
			currUser = currUserEmail.substring(0, currUserEmail.indexOf("@swin.edu.au"));
			sessionStorage.setItem("sessionCurrUser", currUser);
		}else{
			currUser = sessionStorage.getItem("sessionCurrUser");
		}
	}else{
		console.log("Sorry, your browser does not support Web Storage...");
	}

	var hostHrefMap = new Object();
	hostHrefMap['swinburne-dev.instructure.com'] = '/accounts/1/external_tools/470?launch_type=global_navigation';
	hostHrefMap['swinburne-uat.instructure.com'] = '/accounts/1/external_tools/218?launch_type=global_navigation';
	hostHrefMap['swinburne.instructure.com'] = '/accounts/1/external_tools/2136?launch_type=global_navigation';
	hostHrefMap['swinburne.beta.instructure.com'] = '/accounts/1/external_tools/2136?launch_type=global_navigation';
	

	var hasAASlink = false;
	var inboxLi;
		
		
	if(currUser){
			 if(/^[^\d]+[\d]?$/.test(currUser)){
				 
				 //insert logic to add the AAS link
				 
				 var LoopBreakException = {};
				 
			        var menu = $('#menu');
					//console.log("size : " + menu.children().length); 
			        try{
			            for (var i = 0; i < menu.children().length; i++){
			                var li = menu.children()[i];
			                var labelTag = li.getElementsByClassName('menu-item__text');
			                if (labelTag) {
			                    var label = labelTag[0].innerText;
			                   // console.log(label);
			                    if (label && (
			                            label === 'AAS-DEV' ||
			                            label === 'AAS-UAT' ||
			                            label === 'About A Student'
			                            )) {
			                    	hasAASlink = true;
			                    	li.attr("class","globalNavExternalTool menu-item ic-app-header__menu-list-item");
			                    }else{
			                    	if(label &&  label === 'Inbox'){
									   	inboxLi = li;
			                    }
			                }
			            }
					}	
			        } catch(LoopBreakException) {
			        }
			        //console.log("after" + inboxLi);
					var logoAAS = "";
					if(location.hostname === 'swinburne.beta.instructure.com' || location.hostname === 'swinburne.instructure.com'){
						logoAAS = '<img src="https://swinburne.edu.au/app/it-canvas/AASLogo2_white.png" alt=""> <div class="menu-item__text">About A Student</div>';
					}else{
						logoAAS = '<img src="https://swinburne.edu.au/app/it-canvas/AASLogo2.png" alt=""> <div class="menu-item__text">About A Student</div>';
					}					
			        if(!hasAASlink){
			        	// console.log("I am here!");
			        var anchor = $('<a></a>').attr("class","ic-app-header__menu-list-link")
			        		.attr("href",hostHrefMap[location.hostname])
			        		.html(logoAAS);
			        var newLi =  $('<li></li>').attr("class","globalNavExternalTool menu-item ic-app-header__menu-list-item");
			        newLi = newLi.append(anchor);
			     	newLi.insertAfter(inboxLi);
			        }
				 
				
			 }
			 
	}

	
}

$(function(){
	insertAASLink();
});



/*CAN-1995 End  */
/* Removed the code for CAN-2719 (that was to hide AAS LTI for students ) , As its taken care by CAN-1995 */ 