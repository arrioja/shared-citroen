/* IE7 version 0.7.2 (alpha) 2004/08/22 */
if(isHTML){HEADER+="address,blockquote,body,dd,div,dl,dt,fieldset,form,frame,"+"frameset,h1,h2,h3,h4,h5,h6,iframe,noframes,object,p,applet,center,"+"dir,hr,menu,pre{display:block;height:0}li,ol,ul{display:block}"}if(![].push)push=function(array,item){array[array.length]=item;return array.length};if(![].pop)pop=function(array){var item=array[array.length-1];array.length--;return item};if("i".replace(/i/,function(){return""})){var a=String.prototype.replace,b=function(r,w){var m,n="",s=this;while((m=r.exec(s))){n+=s.slice(0,m.index)+w(m[0],m[1],m[2],m[3],m[4]);s=s.slice(m.lastIndex)}return n+s};String.prototype.replace=function(r,w){this.replace=(typeof w=="function")?b:a;return this.replace(r,w)}}
