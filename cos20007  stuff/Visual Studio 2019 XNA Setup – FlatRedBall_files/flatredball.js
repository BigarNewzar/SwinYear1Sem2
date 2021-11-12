var flatredball = (function($) {

    var frb = {};

    frb.init = function () {
        console.log('Initialized flatredball.js.');

        frb.removeApiBaseText();
        frb.decreaseTreeviewIndent();
        frb.doToggleClicks();
        
        // This is no longer being used, links are fixed on the back end
        //frb.applyBrokenLinkHandler();
    };

    // removes long base text such as "FlatRedBall.Camera.Draw"
    // leaving only the end term
    frb.removeApiBaseText = function () {
        var txt;
        $("a.node").each(function () {
            if($(this).text().includes("FlatRedBall") ||
                $(this).text().includes('Microsoft.Xna.Framework.')) {
                txt = $(this).text().replace(/[^ ]+\.([^ ]+)/, "$1");
                $(this).text(txt);
            }
        });
    };

    // decrease the amount of padding each treeview line has
    // pure CSS can't fix this because padding is done with
    // blank images that have no class. This adds a class
    // so they can be fixed with CSS
    frb.decreaseTreeviewIndent = function () {
        $("div.dtNode img").each(function () {
            if($(this).attr('src').includes('empty.gif')) {
                $(this).addClass('emptyImg');
            }
        });
    };

    frb.doToggleClicks = function () {
        $(".frb_toggle_title").click(function () {
            var $body = $(this).siblings(".frb_toggle_body");
            var $symbol = $(this).children(".frb_toggle_symbol");

            if($body.is(':visible')) {
                $symbol.html('[+]');
            }
            else {
                $symbol.html('[-]');
            }
            
            $body.slideToggle("slow");
        })
    }

    // puts custom event handlers on links with old, broken docs links
    // redirects click to a search for the page title
    frb.applyBrokenLinkHandler = function () {
        var $a;
        var href;
        var title;
        var searchBase = '/?s=';
        $("a").each(function () {
            $a = $(this);
            href = $a.attr('href');
            title = $a.attr('title');
            if(href != null && href.includes('frb/docs')) {
                $a.attr('href', searchBase + title);    
            }
        });
    };

    return frb;
}(jQuery));

// bootstrap the app on load
jQuery(function() {
    flatredball.init();
});