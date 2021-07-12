using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;

namespace SlamtruckCarryMyVehicle
{
  public class Class1 : Script
  {
    public List<Class1.VehicleTrailer> Trailers = new List<Class1.VehicleTrailer>();
    public List<Vehicle> ActualTrailers = new List<Vehicle>();
    public Class1.VehicleTrailer CurrentTrailer;
    public Class1.VehicleTrailer Slamtruck;

    public Model RequestModel(string Name)
    {
      Model model = new Model(Name);
      model.Request(10000);
      if (model.IsInCdImage && model.IsValid)
      {
        while (!model.IsLoaded)
          Script.Wait(50);
        return model;
      }
      model.MarkAsNoLongerNeeded();
      return model;
    }

    public Model RequestModel(VehicleHash Name)
    {
      Model model = new Model(Name);
      model.Request(10000);
      if (model.IsInCdImage && model.IsValid)
      {
        while (!model.IsLoaded)
          Script.Wait(50);
        return model;
      }
      model.MarkAsNoLongerNeeded();
      return model;
    }

    public Model RequestModel(PedHash Name)
    {
      Model model = new Model(Name);
      model.Request(10000);
      if (model.IsInCdImage && model.IsValid)
      {
        while (!model.IsLoaded)
          Script.Wait(50);
        return model;
      }
      model.MarkAsNoLongerNeeded();
      return model;
    }

    public Model RequestModel(int Name)
    {
      Model model = new Model(Name);
      model.Request(10000);
      if (model.IsInCdImage && model.IsValid)
      {
        while (!model.IsLoaded)
          Script.Wait(50);
        return model;
      }
      model.MarkAsNoLongerNeeded();
      return model;
    }

    public Class1()
    {
      this.Tick += new EventHandler(this.onTick);
      this.Aborted += new EventHandler(this.OnShutdown);
      this.Interval = 1;
    }

    private void OnShutdown(object sender, EventArgs e)
    {
      if (false)
        return;
      foreach (Class1.VehicleTrailer trailer in this.Trailers)
      {
        if (trailer.CinematicCam != (Camera) null)
        {
          Game.Player.WantedLevel = 0;
          Game.Player.Character.IsInvincible = false;
          Game.Player.Character.FreezePosition = false;
          Function.Call(Hash._0x07E5B515DB0636FC, (InputArgument) 0, (InputArgument) 0, (InputArgument) 3000, (InputArgument) 1, (InputArgument) 0);
          trailer.CinematicCam.IsActive = false;
          trailer.CinematicCam.Destroy();
        }
        if ((Entity) trailer.Door2 != (Entity) null)
          trailer.Door2.Delete();
        if ((Entity) trailer.Door != (Entity) null)
          trailer.Door.Delete();
      }
    }

    private void onTick(object sender, EventArgs e)
    {
      if (this.Slamtruck == null)
        ;
      foreach (Vehicle nearbyVehicle in World.GetNearbyVehicles(Game.Player.Character, 30f))
      {
        if (nearbyVehicle.Model == this.RequestModel("Slamtruck") && (double) nearbyVehicle.Speed < 5.0)
        {
          if (this.ActualTrailers.Count == 0)
          {
            nearbyVehicle.IsPersistent = true;
            this.ActualTrailers.Add(nearbyVehicle);
            this.Trailers.Add(new Class1.VehicleTrailer(nearbyVehicle, 1, false, false));
          }
          if (this.ActualTrailers.Count > 0)
          {
            Class1.VehicleTrailer vehicleTrailer = new Class1.VehicleTrailer(nearbyVehicle, 1, false, false);
            if (!this.ActualTrailers.Contains(nearbyVehicle))
            {
              this.ActualTrailers.Add(nearbyVehicle);
              this.Trailers.Add(vehicleTrailer);
            }
          }
        }
      }
      float num = 9999f;
      foreach (Class1.VehicleTrailer trailer in this.Trailers)
      {
        if ((Entity) trailer.Trailer != (Entity) null && (double) World.GetDistance(Game.Player.Character.Position, trailer.Trailer.Position) < (double) num)
        {
          num = World.GetDistance(Game.Player.Character.Position, trailer.Trailer.Position);
          this.Slamtruck = trailer;
        }
      }
      if (this.Slamtruck != null)
      {
        this.Slamtruck.Trailer.GetOffsetInWorldCoords(new Vector3(0.0f, -2f, 0.75f));
        this.Slamtruck.Trailer.GetOffsetInWorldCoords(new Vector3(1f, -1.75f, 1.25f));
        this.Slamtruck.Trailer.GetOffsetInWorldCoords(new Vector3(-1f, -1.75f, 1.25f));
        this.Slamtruck.Trailer.GetOffsetInWorldCoords(new Vector3(-1f, -1.75f, 1.25f));
        this.Slamtruck.Trailer.GetOffsetInWorldCoords(new Vector3(-1f, -1.75f, 1.25f));
        if (!Game.IsControlPressed(2, Control.VehicleDuck) || !((Entity) Game.Player.Character.LastVehicle != (Entity) null))
          ;
      }
      if (this.Slamtruck != null)
      {
        if (Game.IsControlJustPressed(2, Control.Context))
        {
          if (!this.Slamtruck.AttachedVehicle1)
          {
            if ((double) World.GetDistance(Game.Player.Character.Position, this.Slamtruck.Trailer.Position) < 10.0 && (Entity) Game.Player.Character.CurrentVehicle != (Entity) null)
            {
              Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
              if (currentVehicle.Model != this.RequestModel("SLAMTRUCK") && (currentVehicle.ClassType != VehicleClass.Boats || currentVehicle.ClassType != VehicleClass.Helicopters || currentVehicle.ClassType != VehicleClass.Planes))
              {
                if (currentVehicle.ClassType != VehicleClass.Motorcycles)
                {
                  UI.Notify("Attach 1 Car");
                  this.Slamtruck.Vehicle1 = currentVehicle;
                  this.Slamtruck.AttachedVehicle1 = true;
                  Function.Call(Hash._0x6B9BBD38AB0796DF, (InputArgument) this.Slamtruck.Vehicle1, (InputArgument) this.Slamtruck.Trailer, (InputArgument) 0, (InputArgument) 0.0f, (InputArgument) -2f, (InputArgument) 0.75f, (InputArgument) 10f, (InputArgument) 0.0f, (InputArgument) 0.0f, (InputArgument) 0, (InputArgument) 0, (InputArgument) 1, (InputArgument) 0, (InputArgument) 2, (InputArgument) 1);
                }
                if (currentVehicle.ClassType == VehicleClass.Motorcycles)
                {
                  UI.Notify("Attach 1 Bike ");
                  this.Slamtruck.Vehicle1 = currentVehicle;
                  this.Slamtruck.AttachedVehicle1 = true;
                  Function.Call(Hash._0x6B9BBD38AB0796DF, (InputArgument) this.Slamtruck.Vehicle1, (InputArgument) this.Slamtruck.Trailer, (InputArgument) 0, (InputArgument) 0.8f, (InputArgument) -0.95f, (InputArgument) 0.85f, (InputArgument) 0.0f, (InputArgument) 0.0f, (InputArgument) 0.0f, (InputArgument) 0, (InputArgument) 0, (InputArgument) 1, (InputArgument) 0, (InputArgument) 2, (InputArgument) 1);
                }
              }
            }
          }
          else if (this.Slamtruck.AttachedVehicle1 && (Entity) Game.Player.Character.CurrentVehicle != (Entity) null && (Entity) Game.Player.Character.CurrentVehicle == (Entity) this.Slamtruck.Vehicle1)
          {
            this.Slamtruck.Vehicle1.Detach();
            this.Slamtruck.Vehicle1 = (Vehicle) null;
            this.Slamtruck.AttachedVehicle1 = false;
            UI.Notify("Detach 1");
          }
        }
        if (this.Slamtruck.AttachedVehicle1 && Game.IsControlJustPressed(2, Control.Context))
        {
          if (!this.Slamtruck.AttachedVehicle2)
          {
            if ((Entity) Game.Player.Character.CurrentVehicle != (Entity) null)
            {
              Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
              if ((Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle1 && (currentVehicle.Model != this.RequestModel("SLAMTRUCK") && (currentVehicle.ClassType != VehicleClass.Boats || currentVehicle.ClassType != VehicleClass.Helicopters || currentVehicle.ClassType != VehicleClass.Planes) && currentVehicle.ClassType == VehicleClass.Motorcycles))
              {
                UI.Notify("Attach 2");
                this.Slamtruck.Vehicle2 = currentVehicle;
                this.Slamtruck.AttachedVehicle2 = true;
                Function.Call(Hash._0x6B9BBD38AB0796DF, (InputArgument) this.Slamtruck.Vehicle2, (InputArgument) this.Slamtruck.Trailer, (InputArgument) 0, (InputArgument) -0.8f, (InputArgument) -0.95f, (InputArgument) 0.85f, (InputArgument) 0.0f, (InputArgument) 0.0f, (InputArgument) 0.0f, (InputArgument) 0, (InputArgument) 0, (InputArgument) 1, (InputArgument) 0, (InputArgument) 2, (InputArgument) 1);
              }
            }
          }
          else if (this.Slamtruck.AttachedVehicle2 && (Entity) Game.Player.Character.CurrentVehicle != (Entity) null && (Entity) Game.Player.Character.CurrentVehicle == (Entity) this.Slamtruck.Vehicle2)
          {
            this.Slamtruck.Vehicle2.Detach();
            this.Slamtruck.Vehicle2 = (Vehicle) null;
            this.Slamtruck.AttachedVehicle2 = false;
            UI.Notify("Detach 2");
          }
        }
        if (this.Slamtruck.AttachedVehicle2 && Game.IsControlJustPressed(2, Control.Context))
        {
          if (!this.Slamtruck.AttachedVehicle3)
          {
            if ((Entity) Game.Player.Character.CurrentVehicle != (Entity) null)
            {
              Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
              if ((Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle1 && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle2 && (currentVehicle.Model != this.RequestModel("SLAMTRUCK") && (currentVehicle.ClassType != VehicleClass.Boats || currentVehicle.ClassType != VehicleClass.Helicopters || currentVehicle.ClassType != VehicleClass.Planes) && currentVehicle.ClassType == VehicleClass.Motorcycles))
              {
                UI.Notify("Attach 3");
                this.Slamtruck.Vehicle3 = currentVehicle;
                this.Slamtruck.AttachedVehicle3 = true;
                Function.Call(Hash._0x6B9BBD38AB0796DF, (InputArgument) this.Slamtruck.Vehicle3, (InputArgument) this.Slamtruck.Trailer, (InputArgument) 0, (InputArgument) -0.8f, (InputArgument) -3.25f, (InputArgument) 0.65f, (InputArgument) 13f, (InputArgument) 0.0f, (InputArgument) 0.0f, (InputArgument) 0, (InputArgument) 0, (InputArgument) 1, (InputArgument) 0, (InputArgument) 2, (InputArgument) 1);
              }
            }
          }
          else if (this.Slamtruck.AttachedVehicle3 && (Entity) Game.Player.Character.CurrentVehicle != (Entity) null && (Entity) Game.Player.Character.CurrentVehicle == (Entity) this.Slamtruck.Vehicle3)
          {
            this.Slamtruck.Vehicle3.Detach();
            this.Slamtruck.Vehicle3 = (Vehicle) null;
            this.Slamtruck.AttachedVehicle3 = false;
            UI.Notify("Detach 3");
          }
        }
        if (this.Slamtruck.AttachedVehicle3 && Game.IsControlJustPressed(2, Control.Context))
        {
          if (!this.Slamtruck.AttachedVehicle4)
          {
            if ((Entity) Game.Player.Character.CurrentVehicle != (Entity) null)
            {
              Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
              if ((Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle1 && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle2 && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle3 && (currentVehicle.Model != this.RequestModel("SLAMTRUCK") && (currentVehicle.ClassType != VehicleClass.Boats || currentVehicle.ClassType != VehicleClass.Helicopters || currentVehicle.ClassType != VehicleClass.Planes) && currentVehicle.ClassType == VehicleClass.Motorcycles))
              {
                UI.Notify("Attach 4");
                this.Slamtruck.Vehicle4 = currentVehicle;
                this.Slamtruck.AttachedVehicle4 = true;
                Function.Call(Hash._0x6B9BBD38AB0796DF, (InputArgument) this.Slamtruck.Vehicle4, (InputArgument) this.Slamtruck.Trailer, (InputArgument) 0, (InputArgument) 0.8f, (InputArgument) -3.25f, (InputArgument) 0.65f, (InputArgument) 13f, (InputArgument) 0.0f, (InputArgument) 0.0f, (InputArgument) 0, (InputArgument) 0, (InputArgument) 1, (InputArgument) 0, (InputArgument) 2, (InputArgument) 1);
              }
            }
          }
          else if (this.Slamtruck.AttachedVehicle4 && (Entity) Game.Player.Character.CurrentVehicle != (Entity) null && (Entity) Game.Player.Character.CurrentVehicle == (Entity) this.Slamtruck.Vehicle4)
          {
            this.Slamtruck.Vehicle4.Detach();
            this.Slamtruck.Vehicle4 = (Vehicle) null;
            this.Slamtruck.AttachedVehicle4 = false;
            UI.Notify("Detach 4");
          }
        }
      }
      if (this.Slamtruck == null)
        return;
      if (!this.Slamtruck.AttachedVehicle1)
      {
        if ((Entity) Game.Player.Character.CurrentVehicle != (Entity) null)
        {
          Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
          if (currentVehicle.Model != this.RequestModel("SLAMTRUCK") && (double) World.GetDistance(currentVehicle.Position, this.Slamtruck.Trailer.Position) < 10.0)
          {
            if (currentVehicle.ClassType == VehicleClass.Boats || currentVehicle.ClassType == VehicleClass.Helicopters || currentVehicle.ClassType == VehicleClass.Planes)
              this.DisplayHelpTextThisFrame("You cannot attach this vehicle to the Slamtruck");
            else
              this.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to attach this vehicle to the Slamtruck");
          }
        }
      }
      else if (this.Slamtruck.AttachedVehicle1 && (Entity) Game.Player.Character.CurrentVehicle != (Entity) null && (Entity) Game.Player.Character.CurrentVehicle == (Entity) this.Slamtruck.Vehicle1)
        this.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to detatch this vehicle from the Slamtruck");
      if (this.Slamtruck.AttachedVehicle1)
      {
        if (!this.Slamtruck.AttachedVehicle2)
        {
          if ((Entity) Game.Player.Character.CurrentVehicle != (Entity) null)
          {
            Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
            if (currentVehicle.Model != this.RequestModel("SLAMTRUCK") && (double) World.GetDistance(currentVehicle.Position, this.Slamtruck.Trailer.Position) < 10.0)
            {
              if (currentVehicle.ClassType == VehicleClass.Boats || currentVehicle.ClassType == VehicleClass.Helicopters || currentVehicle.ClassType == VehicleClass.Planes)
              {
                this.DisplayHelpTextThisFrame("You cannot attach this vehicle to the Slamtruck");
              }
              else
              {
                if (currentVehicle.ClassType == VehicleClass.Motorcycles && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle2 && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle1)
                  this.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to attach this vehicle to the Slamtruck");
                if (currentVehicle.ClassType != VehicleClass.Motorcycles && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle1)
                  this.DisplayHelpTextThisFrame("You cannot attach this vehicle to the Slamtruck");
              }
            }
          }
        }
        else if (this.Slamtruck.AttachedVehicle2 && (Entity) Game.Player.Character.CurrentVehicle != (Entity) null && (Entity) Game.Player.Character.CurrentVehicle == (Entity) this.Slamtruck.Vehicle2)
          this.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to detatch this vehicle from the Slamtruck");
      }
      if (this.Slamtruck.AttachedVehicle2)
      {
        if (!this.Slamtruck.AttachedVehicle3)
        {
          if ((Entity) Game.Player.Character.CurrentVehicle != (Entity) null)
          {
            Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
            if (currentVehicle.Model != this.RequestModel("SLAMTRUCK") && (double) World.GetDistance(currentVehicle.Position, this.Slamtruck.Trailer.Position) < 10.0)
            {
              if (currentVehicle.ClassType == VehicleClass.Boats || currentVehicle.ClassType == VehicleClass.Helicopters || currentVehicle.ClassType == VehicleClass.Planes)
              {
                this.DisplayHelpTextThisFrame("You cannot attach this vehicle to the Slamtruck");
              }
              else
              {
                if (currentVehicle.ClassType == VehicleClass.Motorcycles && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle1 && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle2)
                  this.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to attach this vehicle to the Slamtruck");
                if (currentVehicle.ClassType != VehicleClass.Motorcycles && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle1 && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle2)
                  this.DisplayHelpTextThisFrame("You cannot attach this vehicle to the Slamtruck");
              }
            }
          }
        }
        else if (this.Slamtruck.AttachedVehicle3 && (Entity) Game.Player.Character.CurrentVehicle != (Entity) null && (Entity) Game.Player.Character.CurrentVehicle == (Entity) this.Slamtruck.Vehicle3)
          this.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to detatch this vehicle from the Slamtruck");
      }
      if (this.Slamtruck.AttachedVehicle3)
      {
        if (!this.Slamtruck.AttachedVehicle4)
        {
          if ((Entity) Game.Player.Character.CurrentVehicle != (Entity) null)
          {
            Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
            if (currentVehicle.Model != this.RequestModel("SLAMTRUCK") && (double) World.GetDistance(currentVehicle.Position, this.Slamtruck.Trailer.Position) < 10.0)
            {
              if (currentVehicle.ClassType == VehicleClass.Boats || currentVehicle.ClassType == VehicleClass.Helicopters || currentVehicle.ClassType == VehicleClass.Planes)
              {
                this.DisplayHelpTextThisFrame("You cannot attach this vehicle to the Slamtruck");
              }
              else
              {
                if (currentVehicle.ClassType == VehicleClass.Motorcycles && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle1 && ((Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle2 && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle3))
                  this.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to attach this vehicle to the Slamtruck");
                if (currentVehicle.ClassType != VehicleClass.Motorcycles && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle1 && ((Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle2 && (Entity) currentVehicle != (Entity) this.Slamtruck.Vehicle3))
                  this.DisplayHelpTextThisFrame("You cannot attach this vehicle to the Slamtruck");
              }
            }
          }
        }
        else if (this.Slamtruck.AttachedVehicle4 && (Entity) Game.Player.Character.CurrentVehicle != (Entity) null && (Entity) Game.Player.Character.CurrentVehicle == (Entity) this.Slamtruck.Vehicle4)
          this.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to detatch this vehicle from the Slamtruck");
      }
    }

    private void DisplayHelpTextThisFrame(string text)
    {
      Function.Call(Hash._0x8509B634FBE7DA11, (InputArgument) "STRING");
      Function.Call(Hash._0x6C188BE134E074AA, (InputArgument) text);
      Function.Call(Hash._0x238FFE5C7B0498A6, (InputArgument) 0, (InputArgument) 0, (InputArgument) 1, (InputArgument) -1);
    }

    public class VehicleTrailer : Script
    {
      public Vehicle Trailer { get; set; }

      public int Type { get; set; }

      public Prop Door { get; set; }

      public Prop Door2 { get; set; }

      public bool AttachedVehicle1 { get; set; }

      public Vehicle Vehicle1 { get; set; }

      public bool AttachedVehicle2 { get; set; }

      public Vehicle Vehicle2 { get; set; }

      public bool AttachedVehicle3 { get; set; }

      public Vehicle Vehicle3 { get; set; }

      public bool AttachedVehicle4 { get; set; }

      public Vehicle Vehicle4 { get; set; }

      public Vehicle AttachVehicle { get; set; }

      public Camera CinematicCam { get; set; }

      public VehicleTrailer()
      {
      }

      public VehicleTrailer(Vehicle trailer, int type, bool Attached1, bool Attached2)
      {
        this.AttachedVehicle1 = Attached1;
        this.AttachedVehicle2 = Attached2;
        this.Trailer = trailer;
        this.Type = type;
      }
    }
  }
}
