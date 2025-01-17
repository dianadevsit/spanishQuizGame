//create a new scene
//init()>preload()>create()>update()
let gameScene = new Phaser.Scene('Game');

// some parameters for our scene
gameScene.init = function() {}

// load asset files for our game
gameScene.preload = function() {
// load images
  this.load.image('background', 'assets/images/background-city.png');
  this.load.image('building', 'assets/images/building.png');
  this.load.image('car', 'assets/images/car.png');
  this.load.image('house', 'assets/images/house.png');
  this.load.image('tree', 'assets/images/tree.png');
// load audio
  this.load.audio('treeAudio', 'assets/audio/arbol.mp3');
  this.load.audio('carAudio', 'assets/audio/auto.mp3');
  this.load.audio('houseAudio', 'assets/audio/casa.mp3');
  this.load.audio('buildingAudio', 'assets/audio/edificio.mp3');
  this.load.audio('correct', 'assets/audio/correct.mp3');
  this.load.audio('wrong', 'assets/audio/wrong.mp3');
};


// executed once, after assets were loaded
gameScene.create = function() {
  //load background
  let bg = this.add.sprite(0, 0, 'background').setOrigin(0, 0);

  let soundSample = this.sound.add('correct');
  soundSample.play();
  // soundSample.stop();
  // soundSample.pause();
  // soundSample.resume();

};

// our game's configuration
let config = {
  type: Phaser.AUTO,
  width: 640,
  height: 360,
  scene: gameScene,
  title: 'Spanish Learning Game',
  pixelArt: false,
};

// create the game, and pass it the configuration
let game = new Phaser.Game(config);
