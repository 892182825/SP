var allowExt = ['jpg', 'gif', 'bmp', 'png', 'jpeg'];	
var preivew = function(file, container){		
     try{
 	    var pic =  new Picture(file, container);	
     }catch(e){
 	 	    alert(e);		
     }	 
}	 
//缩略图类定义
var Picture  = function(file){
var container= document.getElementById('img');	
var height	= 0, 	
widht= 0, 		
ext	= '',		
size= 0,		
name= '',		
path  	=  '';		
var self 	= this;		
if(file){ 		     
    name = file.value;	
    if (window.navigator.userAgent.indexOf("MSIE")>=1){ 	
        file.select(); 			
        path = document.selection.createRange().text; 		
    }else if(window.navigator.userAgent.indexOf("Firefox")>=1){ 		
        if(file.files){ 		
            path =  file.files.item(0).getAsDataURL(); 				
        }else{		
            path = file.value;	
        }		
    }else {}
    
    }else{ throw "bad file"; } 	
    ext = name.substr(name.lastIndexOf("."), name.length);		
    if(container.tagName.toLowerCase() != 'img'){	
        throw "container is not a valid img label";			 
        container.visibility = 'hidden';		 
    }		
    container.src = path;
    container.alt = name;
    container.style.visibility = 'visible';
    height = container.height;
    widht = container.widht;
    size = container.fileSize;	
    this.get = function(name){
        return self[name];		 
    } 	 	 	 	   			 				 	 	 			 				  				 			  
    this.isValid = function(){	
        if(allowExt.indexOf(self.ext) !== -1){		
            throw 'the ext is not allowed to upload';
            return false;
        }		
    }	
}