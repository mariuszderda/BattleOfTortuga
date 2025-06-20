﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.Payloads
{
    [Serializable]
    public class ShipDestroyedPayload
    {
        private Coord _destroyedShipCoord;
        private Ship _movingShip;
        private int _player1Score;
        private int _player2Score;

        public Coord DestroyedShipCoord
        {
            get { return _destroyedShipCoord; }
            set { _destroyedShipCoord = value; }
        }

        public Ship MovingShip
        {
            get { return _movingShip; }
            set { _movingShip = value; }
        }

        public int Player1Score
        {
            get { return _player1Score; }
            set { _player1Score = value; }
        }
        public int Player2Score
        {
            get { return _player2Score; }
            set { _player2Score = value; }
        }

        public ShipDestroyedPayload()
        {
        }

        public ShipDestroyedPayload(Coord destroyedShipCoord, Ship movingShip, int player1Score, int player2Score)
        {
            _destroyedShipCoord = destroyedShipCoord;
            _movingShip = movingShip;
            _player1Score = player1Score;
            _player2Score = player2Score;
        }

        public void Deconstruct(out object destroyedShipCoord, out object movingShip)
        {
            destroyedShipCoord = this._destroyedShipCoord;
            movingShip = this._movingShip;
        }
    }
}