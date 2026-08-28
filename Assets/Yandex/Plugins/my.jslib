mergeInto(LibraryManager.library, {

SaveExtern: function(date) {
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
  },

  LoadExtern: function() {
    player.getData().then(_date => {
        const myJSON = JSON.stringify(_date);
        myGameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
    })
  },



  });