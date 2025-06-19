using System;
using System.Collections.Generic;

namespace SharedData.Payloads
{
    public class AttackPayload
    {
        private Coord _destroyedShipCoord;
        private Ship _movingShip;

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

        public AttackPayload()
        {
        }

        public AttackPayload(Coord destroyedShipCoord, Ship movingShip)
        {
            _destroyedShipCoord = destroyedShipCoord;
            _movingShip = movingShip;
        }

        public void Deconstruct(out object destroyedShipCoord, out object movingShip)
        {
            destroyedShipCoord = this._destroyedShipCoord;
            movingShip = this._movingShip;
        }
    }
}