$(function () {
	"use strict";
	var tms = tms || {};

	//textarea extensions
	(function () {
		tms.makeAutoGrowTextArea = function ($el) {
			var el = $el[0];
			var minHeight = 50;

			el.style.overflow = "hidden";
			el.style.whiteSpace = "pre-wrap";
			tms.autoGrowTextArea(el, minHeight);

			$el.keyup(function () {
				tms.autoGrowTextArea(this, minHeight);
			});
		};

		tms.autoGrowTextArea = function (el, minHeight) {
			if (el.clientHeight < el.scrollHeight) {
				el.style.height = el.scrollHeight + "px";

				if (el.clientHeight < el.scrollHeight)
					el.style.height = (el.scrollHeight * 2 - el.clientHeight) + "px";
			}
			else if (el.clientHeight > minHeight) {
				if (!(el.style.height)) el.style.height = el.clientHeight + "px";
				var height = parseInt(el.style.height);

				//reducing gradually to avoid IE scroll issue
				while (!(el.clientHeight < el.scrollHeight) && el.clientHeight > minHeight) {
					el.style.height = --height + "px";
				}

				if (el.clientHeight < el.scrollHeight) el.style.height = (++height) + "px";
			}
		};
	})();
});