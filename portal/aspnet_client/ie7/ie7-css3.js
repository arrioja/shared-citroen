/* IE7 version 0.7.2 (alpha) 2004/08/22 */
if(window.IE7)IE7.addModule("ie7-css3",function(){if(!modules["ie7-css2"])return;selectors["~"]=function(filtered,from,filter,scopeName){for(var i=0;i<from.length;i++){var adjacent=from[i];while(adjacent=nextElement(adjacent)){if(adjacent&&compareTagName(adjacent,filter,scopeName))push(filtered,adjacent)}}};var documentElement=(isHTML)?document.documentElement:firstChildElement(document.body);pseudoClasses["root"]=function(element){return Boolean(element==documentElement||element==document.body)};pseudoClasses["empty"]=function(element){return!firstChildElement(element)&&!element.innerText};pseudoClasses["last-child"]=function(element){return!nextElement(element)};pseudoClasses["only-child"]=function(element){return(element.parentNode&&childElements(element.parentNode).length==1)};pseudoClasses["nth-child"]=function(element,filterArgs,step){return nthChild(element,filterArgs,previousElement)};pseudoClasses["nth-last-child"]=function(element,filterArgs){return nthChild(element,filterArgs,nextElement)};function nthChild(element,filterArgs,traverse){switch(filterArgs){case "n":return true;case "even":filterArgs="2n";break;case "odd":filterArgs="2n+1"}var children=childElements(element.parentNode);function checkIndex(index){index=(traverse==nextElement)?children.length-index:index-1;return children[index]==element};if(!isNaN(filterArgs))return checkIndex(filterArgs);filterArgs=filterArgs.split("n");var multiplier=parseInt(filterArgs[0]);var step=parseInt(filterArgs[1]);if(isNaN(multiplier)||(multiplier==1))return true;if(multiplier==0&&!isNaN(step))return checkIndex(step);if(isNaN(step))step=0;var count=(traverse==nextElement)?0:1;while(element=traverse(element))count++;return((count%multiplier)==step)};function childElements(element){var childElements=[],i;for(i=0;i<element.childNodes.length;i++){if(isElement(element.childNodes[i]))push(childElements,element.childNodes[i])}return childElements};attributeTests["^="]=function(attribute,value){return "/^"+unquote(value)+"/.test("+attribute+")"};attributeTests["$="]=function(attribute,value){return "/"+unquote(value)+"$/.test("+attribute+")"};attributeTests["*="]=function(attribute,value){return "/"+unquote(value)+"/.test("+attribute+")"}});
