///to see all codes properly, go to my github link: https://github.com/BigarNewzar/OrbShooterFinal



///(made repository into private, also added licensing )
///(Note: for some reason it isn’t allowing me to upload any files from bin folder. This might cause issues)

using System;

namespace TopDownGame
{
    public static class Program
    {
        [STAThread]
        /// <summary>
        /// Will call game that has been set up as singleton class and run it
        /// </summary>
        static void Main()
        {                         
            Game1 game = Game1.GetInstance();            
            game.Run();            
        }
    }
}





using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownGame
{
    /// <summary>
    /// sets up Game1 by inheriting from its parent preset "Game" class
    /// Also loads up some defaults for spritebatch, soundplayer and spritefont that will be used everywhere else
    /// Also will call mainmenu
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private static Game1 _newGame;
        private SpriteBatch _spriteBatch;
        private MainMenu _mainMenu;
        private SoundPlayer _soundPlayer;
        private SpriteFont _spriteFont;

        /// <summary>
        /// Load graphicdevicemaager. Sets game screen height and width, sets root directory of content to be content folder (to save time and folder looking). Then made sure mousr was visible
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //to set height and width of screen
            _graphics.PreferredBackBufferHeight = Global.screenHeight;
            _graphics.PreferredBackBufferWidth = Global.screenWidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        /// <summary>
        /// let itself get initilize using its parent inbuilt "Game' class
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// loads a spritebatch, soundplayer, spritefont that will be used later on. Also loads all mainmenu object by calling that class
        /// (note: this part can be updated by using gamestate design + a dictionary instead)
        /// </summary>
        protected override void LoadContent()
        {            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _soundPlayer = new SoundPlayer(this);
            _mainMenu = new MainMenu(this);
            _spriteFont = this.Content.Load<SpriteFont>("pic\\Arial16");
        }

        /// <summary>
        /// update itself and game
        /// </summary>
        /// <param name="gameTime">pass the gametime to help mainmenu update itself</param>
        protected override void Update(GameTime gameTime)
        {            
            _mainMenu.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Clear screen and Set background color as black, draw all sprites in spritebatch using begin and end
        /// (note here just calling the mainmenu's things)
        /// </summary>
        /// <param name="gameTime"> to let game draw itself using gametime and its parent class</param>
        /// ref that helped me for spritebatch:https://csharp.hotexamples.com/examples/Microsoft.Xna.Framework.Graphics/SpriteBatch/Begin/php-spritebatch-begin-method-examples.html
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //open spritebatch

            //used sortmode deferred to ensure it doesnt change the order at which sprites are being drawn
            //reference: https://stackoverflow.com/questions/16258200/spritesortmode-texture-sometimes-draw-order-is-wrong
           //used alphablend here mainly cause it sounded cool. it basically blends the colour using alpha chaneela nd a certian equation given in the website link below
            //reference: http://glasnost.itcarlow.ie/~powerk/technology/xna/blending/blending.html
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            //stuff that will be drawn
            _mainMenu.Draw();

            //close sprite batch
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Property to let me access spritebatch from where ever needed
        /// </summary>
        public SpriteBatch SpriteBatch 
        {
            get 
            { 
                return _spriteBatch; 
            } 
        }

        /// <summary>
        /// Property to let me access SoundPlayer from where ever needed
        /// </summary>
        public SoundPlayer SoundPlayer 
        { 
            get 
            { 
                return _soundPlayer; 
            } 
        }

        /// <summary>
        /// Property to let me access SpriteFont from where ever needed
        /// </summary>
        public SpriteFont SpriteFont 
        { 
            get 
            { 
                return _spriteFont; 
            } 
        }

        /// <summary>
        /// used to make the singleton class
        /// </summary>
        /// <returns>instance of Game1</returns>
        public static Game1 GetInstance()
        {
            if (_newGame == null)
            {
                _newGame = new Game1();
            }
            return _newGame;
        }
    }
}





using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownGame
{   
    /// <summary>
    /// Basically this is the main game part
    /// it mainly keeps track of player, mouse, keyboard, enemy, projectile positions and loads background picture and text for player health and point
    /// </summary>
    public class ActiveGameLogic
    {
        private Player _player;
        private KeyboardController _keyboardInputs;
        private EnemySpawner _enemySpawner;
        private CollisionDetector _collisionDetector;        
        private Game1 _game;

        private DrawOnly _gameScreen;
        private int _currentPlayerPoint;
        private int _currentPlayerHealth;

        //to move player via mouse
        private Vector2 _cursorPosition;
        private Vector2 _dPos;

        /// <summary>
        /// gets player's instance, makes new enemy spawner using game passed from MainMenu
        /// also makes new in collision detector 
        /// and makes new keybaord controller using player 
        /// also loads gamescreen picture         
        /// </summary>
        /// <param name="game">used to pass to methods that need it. Also casted it as Game1 to set to _game</param>
        public ActiveGameLogic(Game1 game)
        {
            _player = Player.GetInstance(game);
            _enemySpawner = new EnemySpawner(game);
            _collisionDetector = new CollisionDetector();
            _keyboardInputs = new KeyboardController(_player);

            _game = game;
            _gameScreen = new DrawOnly(new Vector2(400, 400), new Vector2(800, 800), "pic\\activelogicscreen2", game);
            
        }

        /// <summary>
        /// if player isnt expired, then update enemyspawner, collision detector, player and keyboard
        /// store player's health and points in real time to current player health and current player point
        /// passes player to player face mouse method
        /// Also will check if button is being pressed or not and also check if its time to register shoot or not
        /// 
        /// else if player is expired, then also make gamescreen expire
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (_player.IsExpired == false)
            {
                _enemySpawner.Update(gameTime, _player);
                _collisionDetector.Detect(_player, _enemySpawner.EnemyList, _player.ProjectileManager.ProjectileList);
                _player.Update(gameTime);
                _keyboardInputs.Update();

                _currentPlayerPoint = _player.Point;
                _currentPlayerHealth = _player.Health;
                
                PlayerFaceMouse(_player);
                
                //here I decided that the game logic will check if button pressed or not and ensures that even if user keeps pressing down, player will only take the inputs after a certain delay to ensure no cheating
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    if ((gameTime.TotalGameTime.TotalSeconds - _player.LastShot) > Global.shootDelay)
                    {
                        _player.LastShot = gameTime.TotalGameTime.TotalSeconds;
                        _game.SoundPlayer.PlaySoundEffect("playerShootSlow");
                        _player.ProjectileManager.SlowBullet(_player);
                    }
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if ((gameTime.TotalGameTime.TotalSeconds - _player.LastShot) > Global.shootDelay)
                    {
                        _player.LastShot = gameTime.TotalGameTime.TotalSeconds;
                        _game.SoundPlayer.PlaySoundEffect("playerShootNormal");
                        _player.ProjectileManager.NormalBullet(_player);
                    }
                }                
            }
            else 
            {
                _gameScreen.IsExpired = true;                
            }         
        }
        
        /// <summary>
        /// getter property for player
        /// </summary>
        public Player Player { get { return _player; } }


        /// <summary>
        /// to make player rotate with mouse
        /// It gets cursor position, finds difference in player and cursor position 
        /// and sets player's rotation (using math Atan2 to get the angle) and direction
        /// (casting as float to store them into the vector2 and as rotation is float variable but angle is returned as double)
        /// reference: https://docs.microsoft.com/en-us/dotnet/api/system.math.atan2?view=net-5.0
        /// </summary>
        /// <param name="player">to use its position and later to set its rotation and direction</param>
        public void PlayerFaceMouse(Player player)
        {
            _cursorPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            _dPos = player.Pos - _cursorPosition;
            player.Rotation = (float)Math.Atan2(_dPos.Y, _dPos.X);
            player.Direction = new Vector2((float)Math.Cos(player.Rotation), (float)Math.Sin(player.Rotation));
        }

        /// <summary>
        /// As long as gamescreen isnt expired, draw the picture and the text for point and health counter
        /// always draw player and enemyspawner (as other logic are present to remove their drawing)
        /// </summary>
        public void Draw()
        {
            if (_gameScreen.IsExpired == false)
            {
                _gameScreen.Draw(); //putting it first so that it will draw that, then it will draw the rest above it
                _game.SpriteBatch.DrawString(_game.SpriteFont, "Point Counter:" + _currentPlayerPoint, new Vector2(110, 60), Color.Black, 0, new Vector2(0, 0), 1.5f, new SpriteEffects(), 0);
                _game.SpriteBatch.DrawString(_game.SpriteFont, "Player Health:" + _currentPlayerHealth, new Vector2(410, 60), Color.Black, 0, new Vector2(0, 0), 1.5f, new SpriteEffects(), 0);               
            }
            _player.Draw();
            _enemySpawner.Draw();
        }
    }
}





using System;
using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// Made it as a singleton classs as only one player will exist in a game
    /// </summary>
    public class Player : Entity
    {
        private int _collideDamage;
        private static Player _playerInstance;//must keep static here as singleton class
        private double _lastShot;
        private ProjectileManager _projectileManager;
        private int _point;

        /// <summary>
        /// sets its speed, health, collision damage and last shot time
        /// Also sets its direction using its rotation and cos and sin functions (casting them as float)
        /// and instantiates projectile manager (as player is shooting stuff)
        /// </summary>
        /// <param name="position">passes position (of spawning) to parent class</param>
        /// <param name="dimension">passes dimension (ie its size) to parent class</param>
        /// <param name="address">passes picture file location to parent class</param>
        /// <param name="game">passes game to parent class</param>
        public Player(Vector2 position, Vector2 dimension, string address, Game1 game) : base(position, dimension, address, game)
        {
            base.Speed = Global.playerSpeed;            
            base.Health = Global.playerHealth;
            _collideDamage = Global.playerCollideDamage;        //collision will kill all enemy it collides with, but at price of health (see in EnemyPlayerCollisionResponder for details)

            _lastShot = 0.0f;

            this.Direction = new Vector2((float)Math.Cos(this.Rotation), (float)Math.Sin(this.Rotation));

            _projectileManager = new ProjectileManager(game);
           
        }

        /// <summary>
        /// property for lastshot
        /// </summary>
        public double LastShot 
        { 
            get { return _lastShot; } 
            set { _lastShot = value; } 
        }

        /// <summary>
        /// property for collideDamage
        /// </summary>
        public int CollideDamage
        {
            get { return _collideDamage; }
            set { _collideDamage = value; }
        }

        /// <summary>
        /// getter property for projectile manager
        /// </summary>
        public ProjectileManager ProjectileManager 
        {
            get { return _projectileManager; }
        }

        /// <summary>
        /// property for point
        /// </summary>
        public int Point 
        {
            get { return _point; }
            set { _point = value; }
        }

        /// <summary>
        /// when player hits enemy, call this function
        /// it will reduce players health by 1
        /// if player's health is zero of less, play player killed soundeffect and make it expired = true
        /// </summary>
        public void HitEnemy()
        {
            base.Health -= 1;
            if (base.Health <= 0)
            {
                base.SoundPlayer.PlaySoundEffect("playerKilled");
                base.IsExpired = true;
            }
        }

        /// <summary>
        /// makes player go right using its speed
        /// </summary>
        public void GoRight()
        {               
            base.Pos = new Vector2(base.Pos.X + base.Speed, base.Pos.Y);
        }

        /// <summary>
        /// makes player go left using its speed
        /// </summary>
        public void GoLeft()
        {
            base.Pos = new Vector2(base.Pos.X - base.Speed, base.Pos.Y);            
        }

        /// <summary>
        /// makes player go down using its speed
        /// </summary>
        public void GoDown()
        {
            base.Pos = new Vector2(base.Pos.X, base.Pos.Y + base.Speed);
        }

        /// <summary>
        /// makes player go up using its speed
        /// </summary>
        public void GoUp()
        {
            base.Pos = new Vector2(base.Pos.X, base.Pos.Y - base.Speed);
        }

        /// <summary>
        /// as long as player is not expired, update itself using its base and also update its projectileManager
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            if (base.IsExpired == false)
            {
                _projectileManager.Update(gameTime);                
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// aslong as player isnt expired, draw it and its projectile manager
        /// </summary>
        public override void Draw()
        {
            if (base.IsExpired == false)
            {
                _projectileManager.Draw();
                base.Draw();
            }
        }

        /// <summary>
        /// used to make the singleton class
        /// </summary>
        /// <returns>instance of Player</returns>
        public static Player GetInstance(Game1 game)
        {
            if (_playerInstance == null)
            {
                _playerInstance = new Player(new Vector2(300, 300), new Vector2(Global.playerHeight, Global.playerWidth), "pic\\playerBetter",game);//must give it .xnb file!();
            }
            return _playerInstance;
        }
    }
}





namespace TopDownGame
{
    /// <summary>
    /// contains almost all the constants in the game (except the new binding conditions that have been added to match with picture given in active logic)
    /// </summary>
    public class Global
    {
        public const int screenHeight = 800;
        public const int screenWidth = 800;

        public const int projectileHeight = 20;
        public const int projectileWidth = 20;
        public const int projectileRotationVelocity = 3;
        public const int projectileLinearVelocity = 4;
        public const double shootDelay = 0.15;

        public const int projectileSlowDamage = 2;
        public const int projectileSlowSpeed = 5;

        public const int projectileNormalDamage = 1;
        public const int projectileNormalSpeed = 10;

        public const int enemyHeight = 50;
        public const int enemyWidth = 50;
        public const double moveTimeLimit = 2;
        public const double spawnTimeLimit = 1;
        public const float enemyNormalSpeed = 1;
        public const int enemyNormalHealth = 1;
        public const float enemyHomingSpeed = 0.005f;
        public const int enemyHomingHealth = 5;

        public const int playerHeight = 50;
        public const int playerWidth = 50;
        public const int playerSpeed = 2;
        public const int playerHealth = 5;
        public const int playerCollideDamage = 10;

        public const int objectTouchDistance = 50;
    }
}





using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// it inherits from drawonly and gives direction, speed, health and loads soundplayer for things that need it
    /// can be taken as a template that will be heavily modified by its children
    /// </summary>
    public class Entity:DrawOnly
    {
        private Vector2 _direction;
        private float _speed;
        private int _health;
        private Game1 _game;

        /// <summary>
        /// game is passed to _game to make a property that will load in SoundPlayer of game
        /// that way all its child classes can just use its property to get game's sound player
        /// and dont need to keep direct connection between child classes and the game
        /// </summary>
        /// <param name="position">passes position (of spawning) to parent class</param>
        /// <param name="dimension">passes dimension (ie its size) to parent class</param>
        /// <param name="address">passes picture file location to parent class</param>
        /// <param name="game">passes game to parent class</param>
        public Entity(Vector2 position, Vector2 dimension, string address, Game1 game): base(position, dimension, address, game)
        {
            _game = game;            
        }

        /// <summary>
        /// property for speed
        /// </summary>
        public float Speed
        {
            get 
            { 
                return _speed; 
            }
            set
            {
                _speed = value;
            }
        }

        /// <summary>
        /// property for Health
        /// </summary>
        public int Health
        {
            get 
            { 
                return _health; 
            }
            set
            {
                _health = value;
            }
        }

        /// <summary>
        /// getter property for SoundPlayer to get game's soundplayer
        /// </summary>
        public SoundPlayer SoundPlayer
        {
            get
            {
                return _game.SoundPlayer;
            }            
        }

        /// <summary>
        /// property for direction
        /// </summary>
        public Vector2 Direction
        {
            get 
            { 
                return _direction;
            }
            set 
            { 
                _direction = value; 
            }
        }

        /// <summary>
        /// basically does nothing child classes will override it to do something
        /// </summary>
        /// <param name="gameTime">does nothing, child classes will do stuff with it</param>
        public virtual void Update(GameTime gameTime)
        {
            //made it as a template that all child can override it
        }
    }
}






using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownGame
{
    /// <summary>
    /// it will draw the object by using its position, dimension as a texture using game
    /// it will also let it determine if its expired or not and whether it should rotate or not and by how much
    /// </summary>
    public class DrawOnly
    {
        private Vector2 _pos;
        private Vector2 _dim;
        private Texture2D _texture;
        private float _rotation;
        private bool _isExpired;
        private Game1 _game;

        /// <summary>
        /// makes itself using its parameters and sets its expired as false from start
        /// </summary>
        /// <param name="position">passes it to _pos</param>
        /// <param name="dimension">passes it to _dim</param>
        /// <param name="address"> uses it to find where the file is in the content folder</param>
        /// <param name="game"> uses it to call its concent directory to load the file as texture2d</param>
        public DrawOnly(Vector2 position, Vector2 dimension, string address, Game1 game)
        {
            _pos = position;
            _dim = dimension;
            _texture = game.Content.Load<Texture2D>(address);
            //to load the img of player from the file
            _isExpired = false;
            _game = game;           
        }
        /// <summary>
        /// property for rotation for the object
        /// </summary>
        public float Rotation
        {
            get { return _rotation; }

            set
            {
                _rotation = value;
            }
        }

        /// <summary>
        /// property for whether object has expired
        /// </summary>
        public bool IsExpired
        {
            get
            {
                return _isExpired;
            }

            set
            {
                _isExpired = value;
            }
        }

        /// <summary>
        /// property for position of object
        /// </summary>
        public Vector2 Pos
        {
            get { return _pos; }

            set
            {
                _pos = value;
            }
        }

        /// <summary>
        /// property for dimension of object
        /// </summary>
        public Vector2 Dim
        {
            get { return _dim; }

            set
            {
                _dim = value;
            }
        }

        /// <summary>
        /// if texture is present, then draw it using the method given
        /// </summary>
        public virtual void Draw()
        {            
            if (_texture != null)
            {
                //takes in file, draws it as a rect with same position (casting its x and y position and dimensions as int)
                //and size as obj, no source rectangle,
                //and color white (no make sure no extra colour shade will apply to the drawings),
                //and rotation as rotation (so that other objects can set it)
                //sets origin as its width and height devided by 2 to draw from middle
                //currently set the layer depth as 0
               
                _game.SpriteBatch.Draw(_texture, new Rectangle((int)_pos.X, (int)_pos.Y, (int)_dim.X, (int)_dim.Y), null, Color.White, _rotation, new Vector2(_texture.Bounds.Width / 2, _texture.Bounds.Height / 2), new SpriteEffects(), 0);
            }

        }

    }
}





using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace TopDownGame
{
    /// <summary>
    /// Making a SoundPlayer class using strategy/placeholder pattern to make dictionaries for song and soundeffects
    /// and being able to play song, or soundeffect or stop song
    /// </summary>
    public class SoundPlayer
    {        
        private Song1 _currentSong;
        private SoundEffect1 _currentSoundEffect;       
        private Dictionary <string,Song1> _songLibrary;
        private Dictionary<string,SoundEffect1> _soundEffectsLibrary;

        /// <summary>
        /// making dictionaries for song and soundeffects, making individual song and sound effects and adding them to thier respective dictionaries using string to call them
        /// </summary>
        /// <param name="game">passes game to song adn sound effect classes to make themselves</param>
        public SoundPlayer(Game1 game)
        {
            _songLibrary = new Dictionary<string, Song1>();            
            _songLibrary.Add("mainMenu",_currentSong = new Song1("audio\\MainMenuSuspenseMusic", game));
            _songLibrary.Add("activeGame", _currentSong = new Song1("audio\\LostInSoundDropBeat", game));      
            _songLibrary.Add("gameVictory", _currentSong = new Song1("audio\\VictoryScreenHappyMusic", game));

            //.wav file must be loaded as a SoundEffect, .mp3 file must be loaded as a Song
            _soundEffectsLibrary = new Dictionary<string, SoundEffect1>();        
            _soundEffectsLibrary.Add("playerShootNormal", _currentSoundEffect = new SoundEffect1("audio\\LaserPew", game));
            _soundEffectsLibrary.Add("playerShootSlow", _currentSoundEffect = new SoundEffect1("audio\\ProjectileSlow", game));
            _soundEffectsLibrary.Add("enemyKilled", _currentSoundEffect = new SoundEffect1("audio\\EnemyKilled", game));
            _soundEffectsLibrary.Add("playerKilled", _currentSoundEffect = new SoundEffect1("audio\\PlayerKilled", game));        
        }

        /// <summary>
        /// looks though dictionary to find the song and play it
        /// </summary>
        /// <param name="smth">uses this string to compare with other stings in dictionary to find the song</param>
        public void PlaySong(string smth)
        {
              if (_songLibrary.ContainsKey(smth))
                {
                _currentSong = null;
                _currentSong = _songLibrary[smth];
                   
                _currentSong.Play();                   
              }            
        }

        /// <summary>
        /// looks though dictionary to find the soundeffect and play it
        /// </summary>
        /// <param name="smth">uses this string to compare with other stings in dictionary to find the soundeffect</param>
        public void PlaySoundEffect(string smth)
        {
            if (_soundEffectsLibrary.ContainsKey(smth))
            {
                _soundEffectsLibrary[smth].Play();                
            }
        }

        /// <summary>
        /// stops song being played if need to
        /// </summary>
        public void StopMusic()
        {
            MediaPlayer.Stop();
        }
    }
}





using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace TopDownGame
{
    /// <summary>
    /// Loads up song using its file's address and then plays it after determining volume and making repeat true
    /// used IAudio here to ensure it has play method 
    /// (note planned on using IAudio to make it into a dictionary first, but then decided to keep them seperate as i prefer keeping song and soundeffect seperate)
    /// </summary>
    public class SoundEffect1: IAudio
    {
        private SoundEffect _soundEffect;//inbuilt Soundeffect variable to store soundeffects

        /// <summary>
        /// loads the soundeffect from content file directory of game
        /// </summary>
        /// <param name="address">uses file location to make itself</param>
        /// <param name="game">uses game to call its content</param>
        public SoundEffect1(string address, Game1 game)
        {
            _soundEffect = game.Content.Load<SoundEffect>(address);
        }

        /// <summary>
        /// Plays the sound effect using inbuilt SoundEffect class' play method
        /// </summary>
        public void Play()
        {
            _soundEffect.Play();
        }
    }
}





using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace TopDownGame
{
    /// <summary>
    /// Loads up song using its file's address and then plays it after determining volume and making repeat true
    /// used IAudio here to ensure it has play method 
    /// (note planned on using IAudio to make it into a dictionary first, but then decided to keep them seperate as i prefer keeping song and soundeffect seperate)
    /// </summary>
    public class Song1 : IAudio
    {
        private Song _song;//inbuilt Song variable to store songs

        /// <summary>
        /// loads the song from content file directory of game
        /// </summary>
        /// <param name="address">uses file location to make itself</param>
        /// <param name="game">uses game to call its content</param>
        public Song1(string address, Game1 game) 
        {            
            _song = game.Content.Load<Song>(address);
        }

        /// <summary>
        /// sets volume as low value and then plays the song and sets it on repeat
        /// </summary>
        public void Play() 
        {
            MediaPlayer.Volume = 0.25f;//setting the volume to 25 percent for all the music incase too loud
            MediaPlayer.Play(_song);
            MediaPlayer.IsRepeating = true;
        }
    }
}





using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// logic related projectileSlow 
    /// </summary>
    public class ProjectileSlow: Projectile
    {
        /// <summary>
        /// basically sets the speed and damage of the projectile
        /// </summary>
        /// <param name="position">passes position to parent class</param>
        /// <param name="dimension">passes dimension (ie its size) to parent class</param>
        /// <param name="address">passes picture file location to parent class</param>
        /// <param name="game">passes game to parent class</param>
        public ProjectileSlow(Vector2 position, Vector2 dimension, string address, Game1 game)
          : base(position, dimension, address, game)
        {
            base.Damage = Global.projectileSlowDamage;
            base.Speed = Global.projectileSlowSpeed;
        }
    }
}




using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// logic related projectilenormal 
    /// </summary>
    public class ProjectileNormal: Projectile
    {
        /// <summary>
        /// basically sets the speed and damage of the projectile
        /// </summary>
        /// <param name="position">passes position to parent class</param>
        /// <param name="dimension">passes dimension (ie its size) to parent class</param>
        /// <param name="address">passes picture file location to parent class</param>
        /// <param name="game">passes game to parent class</param>
        public ProjectileNormal(Vector2 position, Vector2 dimension, string address, Game1 game)
          : base(position, dimension, address, game)
        {
            base.Damage = Global.projectileNormalDamage;
            base.Speed = Global.projectileNormalSpeed;
        }
    }
}




using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// utilizing concept from factory pattern to make manager class for projectile that decides on what to spawn and where
    /// It also decided when to remove the projectile as well! 
    /// (the "add to different list and then remove from original list" idea was obtained from fruit slasher program that was shown in lecture in the early weeks)
    /// </summary>
    public class ProjectileManager
    {
        private Game1 _game;
        private ProjectileSlow _projectileSlow;
        private ProjectileNormal _projectileNormal;        
        private List<Projectile> _projectileList;
        private List<Projectile> _removeProjectile;

        /// <summary>
        /// creates list (projectile list) to add enemies to, and another list(remove projectile) to constantly remove every projectile stored in it
        /// also allocated game to _game to to ensure i can make the projectile instances in this class
        /// </summary>
        /// <param name="game">passed from active logic to wherever it will be needed here using _game variable</param>
        public ProjectileManager(Game1 game)
        {
            _projectileList = new List<Projectile>();
            _removeProjectile = new List<Projectile>();
            _game = game;
        }

        /// <summary>
        /// getter property for projectilelist
        /// </summary>
        public List<Projectile> ProjectileList 
        {
            get { return _projectileList; }
        }

        /// <summary>
        /// makes a normal projectile, makes its direction same as player and stores it in projectile list
        /// </summary>
        /// <param name="player"> to set initial position and direction of player to the projectile</param>
        public void NormalBullet(Player player)
        {
            _projectileNormal = new ProjectileNormal(player.Pos, new Vector2(Global.projectileHeight, Global.projectileWidth), "pic\\orb1", _game);
            _projectileNormal.Direction = player.Direction;
            //kept both as this as i am looking for player pos and direction

            _projectileList.Add(_projectileNormal);// adding the projectile to the list of  projectiles
        }

        /// <summary>
        /// makes a slow projectile, makes its direction same as player and stores it in projectile list
        /// </summary>
        /// <param name="player"> to set initial position and direction of player to the projectile</param>
        public void SlowBullet(Player player)
        {
            _projectileSlow = new ProjectileSlow(player.Pos, new Vector2(Global.projectileHeight, Global.projectileWidth), "pic\\OrbSlow", _game);
            _projectileSlow.Direction = player.Direction;
            //kept both as this as i am looking for player pos and direction

            _projectileList.Add(_projectileSlow);// adding the projectile to the list of projectiles        
        }

        /// <summary>
        /// if projectile is expired, then remove it from this list ad add it to remvoe projectile list and remove everything present in removeProjectile list
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
             foreach (Projectile p in _projectileList)
             {
                p.Update(gameTime);
                if (p.IsExpired) 
                { 
                    _removeProjectile.Add(p); 
                }

             }
             foreach (Projectile p in _removeProjectile)
             {
                    _projectileList.Remove(p);
             }            
        }

        /// <summary>
        /// draws all projectiles present in projectile list
        /// </summary>
        public void Draw()
        {            
            foreach (Projectile p in _projectileList)
            {
                p.Draw(); 
            }
        }
    }
}





using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// Using inheritance/ template design where the projectile is giving basic idea of what all proejctile can do and know 
    /// but letting subclasses override it to decide what they should actually do
    /// (projectile class will determine all cases on whether an projectile is expired or not)
    /// </summary>
    public class Projectile : Entity
    {
        private int _damage;

        /// <summary>
        /// makes instance of itself using its parent
        /// </summary>
        /// <param name="position">passes position to parent class</param>
        /// <param name="dimension">passes dimension (ie its size) to parent class</param>
        /// <param name="address">passes picture file location to parent class</param>
        /// <param name="game">passes game to parent class</param>
        public Projectile(Vector2 position, Vector2 dimension, string address, Game1 game) : base(position, dimension, address, game)
        {
        }

        /// <summary>
        /// if it hit enemy, then it will call this method which will make its epired value true
        /// </summary>
        public void HitEnemy()
        {
            base.IsExpired = true;
        }

        /// <summary>
        /// Property to get adn set damage values
        /// </summary>
        public int Damage 
        { 
            get 
            {
                return _damage; 
            } 
            set 
            { 
                _damage = value; 
            }
        }

        /// <summary>
        /// makes it remove projectile when hitting wall of picture
        /// otherwise makes it move in the direction of shot
        /// as long as not expired, it will use its parent class to update itself
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (base.Pos.X < 55 || base.Pos.X > 740 || base.Pos.Y < 65 || base.Pos.Y > 770)//added and remvoed 10 to increase projectile shoot distance so that even if player is at wall, he can still shoot properly
            {
                base.IsExpired = true;
            }

            if (base.IsExpired == false)
            {
                base.Update(gameTime);
                this.Pos -= base.Speed * this.Direction;
            }
        }

        /// <summary>
        ///  As long as it isnt expired, it will draw itself using its parent
        /// </summary>
        public override void Draw()
        {
            if (base.IsExpired == false)
            {
                base.Draw();
            }
        }
    }
}




using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    ///  makes all objects present in Menu screen and also its song
    ///  note: can be improved by using gamestate design pattern
    /// </summary>
    public class MainMenu
    {
        private Game1 _game;
        private Button _start;
        private Button _quit;
        private DrawOnly _gameMenuScreen;
        private ActiveGameLogic _newGameLogic;
        private EndScreen _endScreen;
        private bool _gameTrigger;

        /// <summary>
        /// sets game to _game so that it can be passed to things that need it
        /// calls game's soudnplayer to play main menu song
        /// then loads menu screen picture, start and quit buttons and sets game trigger to be false
        /// </summary>
        /// <param name="game">passes it to _game</param>
        public MainMenu(Game1 game) 
        {
            _game = game;
            _game.SoundPlayer.PlaySong("mainMenu");
            _gameMenuScreen = new DrawOnly(new Vector2(400, 400), new Vector2(700, 700), "pic\\MenuScreen1", game);
            _start = new Button(new Vector2(150, 500), new Vector2(150, 150), "pic\\start2", game);
            _quit = new Button(new Vector2(150, 600), new Vector2(150, 150), "pic\\quit2", game);
            _gameTrigger = false;
        }

        /// <summary>
        /// chckes if start and quit button has been clicked or not, constantly
        /// If the game trigger is false, then see if start or quit has been pressed
        /// if start, make activelogic and play its song, and if quit then exit game
        /// if active logic is loaded, then update it, and make everything else expired and set game trigger as true
        /// if player dies in activelogic, then load endscreen and play its song
        /// if end screen is loaded, then update it
        /// note: 
        /// keeping buttons outside as when they have been expired, no matter how much players selects the same position, nothing will happen, so not really needed to keep them inside if statements (as it has already been done to make their expired = true)
        /// </summary>
        /// <param name="gametime"></param>
        public void Update(GameTime gametime)
        {
            _start.Update();
            _quit.Update();

            if (_newGameLogic != null)
            {
                _newGameLogic.Update(gametime);
                _start.IsExpired = true;
                _quit.IsExpired = true;
                _gameMenuScreen.IsExpired = true;
                _gameTrigger = true;


                if (_newGameLogic.Player != null && _newGameLogic.Player.IsExpired && _endScreen == null)
                {
                    _game.SoundPlayer.PlaySong("gameVictory");
                    _endScreen = new EndScreen(_game, _newGameLogic.Player);
                }
            }

            if (_endScreen != null)
            {
                _endScreen.Update();
            }    
         
            if (_gameTrigger == false)
            {
                if (_start.ButtonSelect == true)
                {
                    _newGameLogic = new ActiveGameLogic(_game);
                    _game.SoundPlayer.PlaySong("activeGame");

                }
                if (_quit.ButtonSelect == true)
                {
                    _game.Exit();
                }
            }   
        }
        /// <summary>
        /// if game screen or activelogic or defeatscreen is not expired then draw them respectively,
        /// keeping buttons outside as they will only be drawn if not expired 
        /// </summary>
        public void Draw()
        {
            if (_gameMenuScreen.IsExpired == false)
            { 
                _gameMenuScreen.Draw();
            }
            _start.Draw();
            _quit.Draw();

            if (_newGameLogic != null)
            {
                _newGameLogic.Draw();
            }
            if (_endScreen != null)
            {
                _endScreen.Draw();
            }
        }
    }
}





using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownGame
{
    /// <summary>
    /// makes all objects present in end screen and also uses the point value allocated to the player
    /// note:
    /// currently keeping it silent as only single end screen instead of victory and defeat screen
    /// Also silence seemed to make the player's death soundeffect standout more
    /// </summary>
    public class EndScreen
    {
        private Game1 _game;
        private Player _player;
        private DrawOnly _gameScreen;
        private Button _quit;

        /// <summary>
        /// Takes in game to use its spritebatch later on
        /// Store player as _player to call its point value
        /// draw an image saying gameover, a button for quit and selects font to say the player's point line
        /// </summary>
        /// <param name="game">passes to _game</param>
        /// <param name="player">passed to _player</param>
        public EndScreen(Game1 game, Player player) 
        {
            _game = game;
            _player = player;            
            _gameScreen = new DrawOnly(new Vector2(400, 400), new Vector2(800, 800), "pic\\Gameover", game);
            _quit = new Button(new Vector2(200, 650), new Vector2(150, 150), "pic\\quit2", game);
        }

        /// <summary>
        /// continuously checked if button has been clicked or not
        /// and if it has then exits game
        /// </summary>
        public void Update()
        {
            _quit.Update();//checks the button click condition and determines whether buttonselect should change
            if (_quit.ButtonSelect == true)
            {
                _game.Exit();
            }            
        }

        /// <summary>
        /// draws the game screen picture first
        /// then it draws the line to say no of points player got in the preselected font
        /// then sets its position, colour, origin, spriteeffect and layer depth
        /// mainly used this overload to maximize the font so kept o for origin and depth and just made a new sprite effect to ensure the string can be drawn without having any effect whatsoever
        /// </summary>
        public void Draw()
        {            
            _gameScreen.Draw();
            _game.SpriteBatch.DrawString(_game.SpriteFont, "You have obtained: " + _player.Point +" points!", new Vector2(150, 500), Color.White, 0, new Vector2(0,0), 2, new SpriteEffects(), 0);//made origin 0,0 just for ease of use and thus only need to change position
            _quit.Draw();
        }
    }
}





using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TopDownGame
{
    /// <summary>
    /// made it as a seperate classes so that i can set all button stuff from one class
    /// (note: can be modified later to add more functonality to buttons, like a box or hover effect thing)
    /// </summary>
    public class Button: DrawOnly
    {
        private bool _buttonSelect;
        private Vector2 _cursorPosition;//inbuilt vector2 class, used to utilize x,y position of mouse

        /// <summary>
        /// uses parent class to make and draw itself
        /// Also sets button select as false from start
        /// </summary>
        /// <param name="position">passes position to parent</param>
        /// <param name="dimension">passes dimension (ie size) to parent</param>
        /// <param name="address">passes location of picture file to parent</param>
        /// <param name="game">passes game to parent</param>
        public Button(Vector2 position, Vector2 dimension, string address, Game1 game) : base(position, dimension, address, game)
        {
            _buttonSelect = false;
        }

        /// <summary>
        /// Property to get whether button select is true or false
        /// </summary>
        public bool ButtonSelect 
        {
            get { return _buttonSelect; }
        }

        /// <summary>
        /// if not expired, uses base to draw itself
        /// </summary>
        public override void Draw()
        {
            if (base.IsExpired == false)
            {
                base.Draw();
            }           
        }

        /// <summary>
        /// it takes in position of cursor as a vector. Then it checkes whether the position is really in the boundaries of the button and if clikced with left button or not. If so, it makes button select as true
        /// </summary>
        public virtual void Update()
        {
            _cursorPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
           
            if (_cursorPosition.X > base.Pos.X - base.Dim.X/2 && _cursorPosition.X < base.Pos.X + base.Dim.X/2 && _cursorPosition.Y > base.Pos.Y - base.Dim.Y/2 && _cursorPosition.Y < base.Pos.Y + base.Dim.Y/2 && Mouse.GetState().LeftButton == ButtonState.Pressed)//condition used to set up button's boundary box
            {                
                _buttonSelect = true;
            }
        }
    }
}





using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// utilizing concept from factory pattern to make spawner class that randomly decides on what to spawn
    /// It also decided when to remove the enemy as well! 
    /// (the "add to different list and then remove from original list" idea was obtained from fruit slasher program that was shown in lecture in the early weeks)
    /// </summary>
    public class EnemySpawner
    {
        private EnemyNormal _enemyNormal;
        private EnemyHoming _enemyHoming;
        private List<Enemy> _enemyList;
        private List<Enemy> _removeEnemy;
        private Game1 _game;
        private double _oldSpawnTime;//checks old spawn time to determine to spawn new one or not

        /// <summary>
        /// creates list (enemy list) to add enemies to, and another list(remove enemy) to constantly remove every enemy stored in it
        /// also set starting spawn time as zero
        /// also allocated game to _game to to ensure i can make the enemy instances in this class
        /// </summary>
        /// <param name="game">passed from active logic to wherever it will be needed here using _game variable</param>
        public EnemySpawner(Game1 game) 
        {
            _enemyList = new List<Enemy>();
            _oldSpawnTime = 0;
            _removeEnemy = new List<Enemy> ();
            _game = game;
        }
        /// <summary>
        /// getter property in case any classes need to get it
        /// </summary>
        public List<Enemy> EnemyList
        {
            get { return _enemyList; }
        }

        /// <summary>
        /// Spawns a normal enemy not at the same player position and adds it to list of enemies
        /// (note it just ensures its not spawning directly on player, but it might still spawn within player touch distance and thus, if player in unlucky, he might lose a life without knowing it (unless he hears the enemy death sound))
        /// can be refactored to remove player, but then will make the update "if" heavy
        /// will probably use a dictionary instead for this next time
        /// </summary>
        /// <param name="x"> x position used to spawn enemy</param>
        /// <param name="y"> y position used to spawn enemy</param>
        /// <param name="player">passed in player from update</param>
        public void SpawnEnemyNormal(int x, int y, Player player)
        {
            if (x != player.Pos.X && y != player.Pos.Y)
            {
                _enemyNormal = new EnemyNormal(new Vector2(x, y), new Vector2(Global.enemyHeight, Global.enemyWidth), "pic\\EnemyTest",_game);

                _enemyList.Add(_enemyNormal);
            }
        }

        /// <summary>
        /// Spawns a normal enemy not at the same player position and adds it to list of enemies
        /// (note it just ensures its not spawning directly on player, but it might still spawn within player touch distance and thus, if player in unlucky, he might lose a life without knowing it (unless he hears the enemy death sound))
        /// can be refactored to remove player, but then will make the update "if" heavy
        /// will probably use a dictionary instead for this next time
        /// </summary>
        /// <param name="x"> x position used to spawn enemy</param>
        /// <param name="y"> y position used to spawn enemy</param>
        /// <param name="player">passed in player from update</param>
        public void SpawnEnemyHoming(int x, int y,Player player)
        {
            if (x != player.Pos.X && y != player.Pos.Y)
            {
                _enemyHoming = new EnemyHoming(new Vector2(x, y), new Vector2(Global.enemyHeight, Global.enemyWidth), "pic\\EnemyHoming", _game);

                _enemyList.Add(_enemyHoming);
            }
        }

        /// <summary>
        /// spawns certain enemy at certain times randomly
        /// also says how to remove the enemy from enemyist and add to removeEnemy and then remove all enemyes in removeEnemy and also added 1 point to player as enemy got destroyed
        /// </summary>
        /// <param name="gameTime"> passes in gametime from avtice logic</param>
        /// <param name="player">passes in player from active logic</param>
        public void Update(GameTime gameTime, Player player) 
        {
            if (gameTime.TotalGameTime.TotalSeconds - _oldSpawnTime > Global.spawnTimeLimit)
            {
                _oldSpawnTime = gameTime.TotalGameTime.TotalSeconds;
                //use random to make it so that 50 percent time spawn normal, 50 percent time spawn homing enemy

                if (new Random().Next(0, 10) < 5)
                {
                    SpawnEnemyNormal(new Random().Next(120, 700), new Random().Next(120, 700), player);
                }
                else
                {
                    SpawnEnemyHoming(new Random().Next(120, 700), new Random().Next(120, 700), player);
                }
            }

            foreach (Enemy e in _enemyList)
            {
                e.Update(gameTime); 

                if (e.IsExpired)
                {
                    _removeEnemy.Add(e);
                    player.Point += 1;  //gives player 1 poit no matter what enemy he kills as i already made proper balancing to both enemies to even them out!
                }

            }

            foreach (Enemy e in _removeEnemy)
            {
                _enemyList.Remove(e);
            }            
        }

        /// <summary>
        /// draws all enemies present in enemylist
        /// </summary>
        public void Draw()
        {
            foreach (Enemy e in _enemyList)
            {
                e.Draw();
            }
        }
    }
}





using System;
using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// logic related enemynormal movement and draw
    /// </summary>
    public class EnemyNormal : Enemy
    {
        private float _x;
        private float _y;
        private double _oldMoveTime;//checks old move time for movement condition

        /// <summary>
        /// basically set up the logic for how homing enemy will move itself 
        /// (it will draw using parent so not placing it here)
        /// Also setting its speed and health using global values
        /// Also setting its oldmovetime to 0 to set up random move condition
        /// and passing the current game to active logic so that it can track the "player" in the active logic and find its position
        /// <param name="position">passes position (of spawning) to parent class</param>
        /// <param name="dimension">passes dimension (ie its size) to parent class</param>
        /// <param name="address">passes picture file location to parent class</param>
        /// <param name="game">passes game to parent class</param>
        public EnemyNormal(Vector2 position, Vector2 dimension, string address, Game1 game) : base(position, dimension, address, game)
        {
            base.Speed = Global.enemyNormalSpeed;
            base.Health = Global.enemyNormalHealth;
            _oldMoveTime = 0;
        }

        /// <summary>
        /// changes x and y value to random float between -5 to 5
        /// </summary>
        public void CalcuateNewMove()
        {
            //create a random x and y values to be passed to position
            _x = new Random().Next(-5, 5);
            _y = new Random().Next(-5, 5);
        }

        /// <summary>
        /// sees if enemy hits screen (altered it to match picture + keep space for extra game text at the top during game)
        /// or not
        /// </summary>
        /// <returns> if hits screen then true, else false</returns>
        public bool EnemyHitScreen()
        {           
             if ((this.Pos.Y <= 110) || (this.Pos.Y >= 760) || (this.Pos.X <= 60) || (this.Pos.X >= 730))
             {
                return true;
             }
            else { return false; }
        }

        /// <summary>
        /// says the general enemy movement
        /// Sets condition to switch movement after certain period of time
        /// (also store the gametime now into oldmovetime to recalculate later on)
        /// Also if it hits the wall, it will bounce back in the opposite direction
        ///  made sure it also updates its parent after overriding itself incase parent has had any extra logic that it wants to impliment as well (just to be on safe side)
        /// </summary>
        /// <param name="gameTime">passes gametime to parent class</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //condition to switch enemy movement
            if (gameTime.TotalGameTime.TotalSeconds - _oldMoveTime > Global.moveTimeLimit)
            {
                _oldMoveTime = gameTime.TotalGameTime.TotalSeconds;
                CalcuateNewMove();
            }

            this.Pos += new Vector2(_x, _y) * Speed;  //enemy movement in general

            if (EnemyHitScreen())
            {
                _x = -_x;
                _y = -_y;
            }            
        }    
    }
}





using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// logic related enemyhoming movement and draw
    /// </summary>
    public class EnemyHoming : Enemy
    {     
        private ActiveGameLogic _active;//the active logic will tell the homing enemy of the player's position and thus help it "home" to the player

        /// <summary>
        /// basically set up the logic for how homing enemy will move itself 
        /// (it will draw using parent so not placing it here)
        /// Also setting its speed and health using global values
        /// and passing the current game to active logic so that it can track the "player" in the active logic and find its position
        /// <param name="position">passes position (of spawning) to parent class</param>
        /// <param name="dimension">passes dimension (ie its size) to parent class</param>
        /// <param name="address">passes picture file location to parent class</param>
        /// <param name="game">passes game to parent class</param>
        public EnemyHoming(Vector2 position, Vector2 dimension, string address, Game1 game) : base(position, dimension, address, game)
        {
            base.Speed = Global.enemyHomingSpeed;
            base.Health = Global.enemyHomingHealth;

            _active = new ActiveGameLogic(game);
        }

        /// <summary>
        /// made a funny homing movement + made sure it also updates its parent after overriding itself incase parent has had any extra logic that it wants to impliment as well (just to be on safe side)
        /// </summary>
        /// <param name="gameTime">passes gametime to parent class</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.Pos += (_active.Player.Pos - this.Pos) * Speed;//finds difference in position vector2 and multiplies with speed. Thus if far away, enemy runs towards player, if close, enemy moves slowly to player. This ensures player has a decent chance to kill the homing one as it has high health
        }

    }
}




using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// Using inheritance/ template design where the enemy is giving basic idea of what all enemy can do and know 
    /// but letting subclasses override it to decide what they should actually do
    /// (enemy class will determine all cases on whether an enemy is expired or not)
    /// </summary>
    public class Enemy : Entity
    {
        /// <summary>
        /// it is basically a template that every enemy will follow. Here all enemies know what to do if they are hit and how to update and draw themselves
        /// </summary>
        /// <param name="position">passes position (of spawning) to parent class</param>
        /// <param name="dimension">passes dimension (ie its size) to parent class</param>
        /// <param name="address">passes picture file location to parent class</param>
        /// <param name="game">passes game to parent class</param>
        public Enemy(Vector2 position, Vector2 dimension, string address, Game1 game) : base(position, dimension, address, game)
        {            
        }

        /// <summary>
        /// it decrease its health depending on value of projectile damage or player collide damage passed to it
        /// then if its health is less or equal to zero, it will be expired and play soundeffect for enemykilled
        /// </summary>
        /// <param name="n"> the number passed from enemyprojectile collision and enemyplayer collision responder classes</param>
        public void WasHit(int n)
        {
            base.Health -= n;

            if (base.Health <= 0)
            {
                base.IsExpired = true;
                base.SoundPlayer.PlaySoundEffect("enemyKilled");
            }
        }

        /// <summary>
        /// as long as not expired, it will use its parent class to update itself
        /// </summary>
        /// <param name="gameTime">it will use gametime passed from enemy spawner to update itself</param>
        public override void Update(GameTime gameTime)
        {
            if (base.IsExpired == false)
            {
                base.Update(gameTime);
            }
        }
            
        /// <summary>
        /// As long as it isnt expired, it will draw itself using its parent
        /// </summary>
        public override void Draw()
        {
            if (base.IsExpired == false)
            {
                base.Draw();
            }           
        }
    }
}





namespace TopDownGame
{
    /// <summary>
    /// Basically it will pefrom its task when it gets signal that any projectile has hit any enemy
    /// </summary>
    public class EnemyProjectileCollisionResponder
    {
        public EnemyProjectileCollisionResponder()
        {
        }

        /// <summary>
        /// it will make projectile disappear upon contact and ensure enemy's health decrease 
        /// by the damage allocated to that projectile. It calls the projectile's hitenemy method 
        /// and passes its damage value to enemy's washit method
        /// </summary>
        /// <param name="p">Passes in projectile from Collision Detector's projectilelist</param>
        /// <param name="e">Passes in enemy from Collision Detector's enemylist</param>
        public void EnemyProjectileCollide(Projectile p, Enemy e)
        {
            p.HitEnemy();
            e.WasHit(p.Damage);
        }
    }
}






namespace TopDownGame
{
    /// <summary>
    /// Basically it will pefrom its task when it gets signal that player had hit any enemy
    /// </summary>
    public class EnemyPlayerCollisionResponder
    {
        public EnemyPlayerCollisionResponder()
        {            
        }

        /// <summary>
        /// it performs player's HitEnemy method which will decrease player Health by 1 (no matter what enemy)
        /// it will destroy the enemy no matter what type (as i set a very high collide damage value for player 
        /// + no invulnerability frame has been setup, so will player drain all enemy health almost instantly upon contact)
        /// </summary>
        /// <param name="player">Passes in player from Collision Detector</param>
        /// <param name="e">Passes in enemy from Collision Detector's enemylist</param>
        public void EnemyPlayerCollide(Player player, Enemy e)
        {
            player.HitEnemy();
            e.WasHit(player.CollideDamage); 
        }
    }
}





using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TopDownGame
{
    /// <summary>
    /// Utilizing observer/ detector/ responder pattern to see if collision took place between player and enemy or enemy and projectile and if so, do needed tasks
    /// </summary>
    public class CollisionDetector
    {   
        private EnemyPlayerCollisionResponder _enemyPlayerCollideResponder;
        private EnemyProjectileCollisionResponder _enemyProjectileCollideResponder;

        /// <summary>
        /// make instances of the two types of responder
        /// </summary>
        public CollisionDetector()
        {
            _enemyPlayerCollideResponder = new EnemyPlayerCollisionResponder();
            _enemyProjectileCollideResponder = new EnemyProjectileCollisionResponder();
        }

        /// <summary>
        /// detect what had happened for all projectile and enemies and call certain functions from responder if in touch distance
        /// </summary>
        /// <param name="player">passes player from activeGameLogic to responder for player enemy collision</param>
        /// <param name="enemyList">passes list of enemies from activeGameLogic to responder for player enemy collision,
        /// and enemy and projectile collision</param>
        /// <param name="projectileList">passes list of projectiles from activeGameLogic to responder for projectile enemy collision</param>
        public void Detect(Player player, List<Enemy> enemyList, List<Projectile> projectileList)
        {
            foreach (Enemy e in enemyList)
            {
                if (Vector2.Distance(player.Pos, e.Pos) <= Global.objectTouchDistance)//basically when distance between them is 50 or less
                {
                    _enemyPlayerCollideResponder.EnemyPlayerCollide(player, e);
                }

                foreach (Projectile p in projectileList)
                {
                    if (Vector2.Distance(p.Pos, e.Pos) <= Global.objectTouchDistance)//basically when distance between them is 50 or less
                    {
                        _enemyProjectileCollideResponder.EnemyProjectileCollide(p, e);                        
                    }
                }
            }

        }
    }
}






namespace TopDownGame
{
    /// <summary>
    ///Used it to make it undertand all commands as single type of object (ie of ICommand type)
    ///and ensured they all had execute method that took no parameter and returned void
    ///this helped me make the dictionary and strategy pattern for keyboard controller
    /// </summary>
    public interface ICommands
    {
        void Execute();
    }
}






namespace TopDownGame
{
    /// <summary>
    /// At first i did it to use to make a dictionary for both sing and soundeffect
    /// But it felt wrong to include both long music and short sound together
    /// 
    /// So for now, i just kept it to ensure that both soundeffect and song will have the play method that returns nothing
    /// just to make sure i dont forget to add "play" method to those classes while i am multitasking
    /// </summary>
    public interface IAudio
    {
        void Play();
    }
}





using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;


namespace TopDownGame
{
    /// <summary>
    ///making a strategy pattern to hotbind keys to commands
    /// </summary>
    public class KeyboardController 
    {   
        private KeyboardState _keyboardState;//inbuilt class to call the keys
        private Player _player;
        private ICommands _currentCommand;
        private Dictionary<Keys, ICommands> _commandLibrary;

        /// <summary>
        /// making a dictionary binding keys to their respective commands using the strategy pattern
        /// </summary>
        /// <param name="player">Passing player received from ActiveGameLogic to the commands</param>
        public KeyboardController(Player player)
        {
            this._player = player;
            _commandLibrary = new Dictionary<Keys, ICommands>();
            _commandLibrary.Add(Keys.W, _currentCommand = new UpCommand(_player));
            _commandLibrary.Add(Keys.Up, _currentCommand = new UpCommand(_player));
            _commandLibrary.Add(Keys.S, _currentCommand = new DownCommand(_player));
            _commandLibrary.Add(Keys.Down, _currentCommand = new DownCommand(_player));
            _commandLibrary.Add(Keys.A, _currentCommand = new LeftCommand(_player));
            _commandLibrary.Add(Keys.Left, _currentCommand = new LeftCommand(_player));
            _commandLibrary.Add(Keys.D, _currentCommand = new RightCommand(_player));
            _commandLibrary.Add(Keys.Right, _currentCommand = new RightCommand(_player));
            _commandLibrary.Add(Keys.Q, _currentCommand = new QuitCommand());
        }

        /// <summary>
        /// updating the command received
        /// setting it first as null then taking keyboard state and looking through dictionary to find and execute the command
        /// </summary>
        public void Update()
        {
            _currentCommand = new NullCommand(); 
            _keyboardState = Keyboard.GetState();
            foreach (Keys key in _keyboardState.GetPressedKeys())
            {
                if (_commandLibrary.ContainsKey(key))
                {
                    _currentCommand = _commandLibrary[key];
                    _currentCommand.Execute();
                }
            }
        }
    }
}






namespace TopDownGame
{
    /// <summary>
    /// Example of composition, using ICommands mainly to add it to dictionary in keyboardController
    /// </summary>
    public class UpCommand : ICommands
    {   
        private Player _player;

        /// <summary>
        /// basically allocated player to _player
        /// </summary>
        /// <param name="player">to use the player passed from keyboard controller</param>
        public UpCommand(Player player)
        {
            this._player = player;
        }

        /// <summary>
        /// setting condition to restrict when it can go up and if condition fulfilled, then tell player to call its GoUp method
        /// </summary>
        public void Execute()
        {
            if (_player.Pos.Y >110)
            {
                _player.GoUp();
            }
        }
    }
}






namespace TopDownGame
{   /// <summary>
    /// Example of composition, using ICommands mainly to add it to dictionary in keyboardController
    /// </summary>
    public class RightCommand : ICommands
    {
        private Player _player;

        /// <summary>
        /// basically allocated player to _player
        /// </summary>
        /// <param name="player">to use the player passed from keyboard controller</param>
        public RightCommand(Player player)
        {
            this._player = player;
        }

        /// <summary>
        /// setting condition to restrict when it can go right and if condition fulfilled, then tell player to call its GoRight method
        /// </summary>
        public void Execute()
        {
            if (_player.Pos.X < 730)
            {
                _player.GoRight();
            }
        }
    }
}






namespace TopDownGame
{   /// <summary>
    /// Example of composition, using ICommands mainly to add it to dictionary in keyboardController
    /// basically does nothing other than exiting game
    /// </summary>
    public class QuitCommand : ICommands
    {
        
        public QuitCommand()
        {
        }
        /// <summary>
        /// exits game by calling game1's getinstance and exit 
        /// </summary>
        public void Execute()
        {
            Game1.GetInstance().Exit();
        }
    }
}






namespace TopDownGame
{   /// <summary>
    /// Example of composition, using ICommands mainly to add it to dictionary in keyboardController
    /// Basically, here i am using it as placeholder for strategy pattern
    /// it isnt going to do anything at all
    /// </summary>
    public class NullCommand : ICommands
    {
        public NullCommand()
        {
        }
        public void Execute()
        {
        }
    }
}






namespace TopDownGame
{   /// <summary>
    /// Example of composition, using ICommands mainly to add it to dictionary in keyboardController
    /// </summary>
    public class LeftCommand : ICommands
    {
        private Player _player;

        /// <summary>
        /// basically allocated player to _player
        /// </summary>
        /// <param name="player">to use the player passed from keyboard controller</param>
        public LeftCommand(Player player)
        {
            this._player = player;
        }

        /// <summary>
        /// setting condition to restrict when it can go Left and if condition fulfilled, then tell player to call its GoLeft method
        /// </summary>
        public void Execute()
        {
            if (_player.Pos.X > 60)
            {
                _player.GoLeft();
            }
        }
    }
}






namespace TopDownGame
{
    /// <summary>
    /// Example of composition, using ICommands mainly to add it to dictionary in keyboardController
    /// </summary>
    public class DownCommand : ICommands
    {
        Player _player;

        /// <summary>
        /// basically allocated player to _player
        /// </summary>
        /// <param name="player">to use the player passed from keyboard controller</param>
        public DownCommand(Player player)
        {
            this._player = player;
        }
        /// <summary>
        /// setting condition to restrict when it can go down and if condition fulfilled, then tell player to call its GoDown method
        /// </summary>
        public void Execute()
        {
            if (_player.Pos.Y < 760)
            {
                _player.GoDown();
            }
        }
    }
}





