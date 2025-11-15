// import React from "react";
// import "./Report.css";

// const Report: React.FC = () => {
//   return <></>;
// };

// export default Report;

// import React, { useState } from "react";
// import {
//   TextField,
//   Button,
//   Card,
//   CardContent,
//   Typography,
//   MenuItem,
// } from "@mui/material";
// import "./Report.css";

// const incidentTypes = [
//   {
//     value: "Parcat pe spatiu verde/bordura",
//     label: "Parcat pe spatiu verde/bordura",
//   },
//   { value: "Parcat in fata unei porti", label: "Parcat in fata unei porti" },
//   { value: "Accident", label: "Accident" },
//   { value: "Parcat pe sosea", label: "Parcat pe sosea" },
//   { value: "other", label: "Other" },
// ];

// const Report: React.FC = () => {
//   const [uploadedImage, setUploadedImage] = useState<string | null>(null);

//   const handleImageUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
//     const file = event.target.files?.[0];
//     if (file) {
//       setUploadedImage(URL.createObjectURL(file));
//     }
//   };

//   return (
//     <div className="report-container">
//       <Card className="report-card">
//         <CardContent>
//           <Typography variant="h4" className="report-title">
//             Report an Incident
//           </Typography>

//           {/* Incident Type */}
//           <TextField
//             select
//             label="Incident Type"
//             fullWidth
//             className="report-field"
//             defaultValue=""
//           >
//             {incidentTypes.map((type) => (
//               <MenuItem key={type.value} value={type.value}>
//                 {type.label}
//               </MenuItem>
//             ))}
//           </TextField>

//           {/* Location */}
//           <TextField label="Location" fullWidth className="report-field" />

//           {/* Vehicle Plate */}
//           <TextField
//             label="License Plate (optional)"
//             fullWidth
//             className="report-field"
//           />

//           {/* Upload Image */}
//           <div className="upload-box">
//             <input
//               accept="image/*"
//               type="file"
//               id="fileUpload"
//               hidden
//               onChange={handleImageUpload}
//             />
//             <label htmlFor="fileUpload">
//               <Button variant="outlined" component="span" fullWidth>
//                 Upload Image
//               </Button>
//             </label>

//             {uploadedImage && (
//               <img
//                 src={uploadedImage}
//                 alt="preview"
//                 className="image-preview"
//               />
//             )}
//           </div>

//           {/* Description */}
//           <TextField
//             label="Description"
//             fullWidth
//             multiline
//             rows={4}
//             className="report-field"
//           />

//           <Button variant="contained" color="primary" fullWidth>
//             Submit Report
//           </Button>
//         </CardContent>
//       </Card>
//     </div>
//   );
// };

// export default Report;

import React, { useState } from "react";
import {
  TextField,
  Button,
  Card,
  CardContent,
  Typography,
  MenuItem,
  Dialog,
  DialogContent,
  DialogTitle,
} from "@mui/material";
import { MapContainer, TileLayer, Marker, useMapEvents } from "react-leaflet";
import L from "leaflet";
import "leaflet/dist/leaflet.css";
import "./Report.css";

// Fix pentru iconița marker-ului leaflet
delete (L.Icon.Default.prototype as any)._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon-2x.png",
  iconUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon.png",
  shadowUrl:
    "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png",
});

const incidentTypes = [
  {
    value: "Parcat pe spatiu verde/bordura",
    label: "Parcat pe spatiu verde/bordura",
  },
  { value: "Parcat in fata unei porti", label: "Parcat in fata unei porti" },
  { value: "Accident", label: "Accident" },
  { value: "Parcat pe sosea", label: "Parcat pe sosea" },
  { value: "other", label: "Other" },
];

const Report: React.FC = () => {
  const [uploadedImage, setUploadedImage] = useState<string | null>(null);
  const [location, setLocation] = useState("");
  const [openMap, setOpenMap] = useState(false);
  const [position, setPosition] = useState<[number, number]>([
    45.7489, 21.2087,
  ]);
  const [tempAddress, setTempAddress] = useState("");

  const handleImageUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      setUploadedImage(URL.createObjectURL(file));
    }
  };

  // Componentă pentru gestionarea click-urilor pe hartă
  const MapClickHandler = () => {
    useMapEvents({
      click: async (e) => {
        const { lat, lng } = e.latlng;
        setPosition([lat, lng]);

        // Reverse geocoding pentru a obține adresa
        try {
          const response = await fetch(
            `https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lng}&addressdetails=1`
          );
          const data = await response.json();

          const street = data.address.road || "";
          const houseNumber = data.address.house_number || "";
          const locationStr = `${street}${
            houseNumber ? " " + houseNumber : ""
          }, Timișoara`;

          setTempAddress(locationStr);
        } catch (error) {
          console.error("Eroare la obținerea adresei:", error);
          setTempAddress(`Lat: ${lat.toFixed(4)}, Lng: ${lng.toFixed(4)}`);
        }
      },
    });
    return null;
  };

  const handleConfirmLocation = () => {
    setLocation(tempAddress);
    setOpenMap(false);
  };

  return (
    <div className="report-container">
      <Card className="report-card">
        <CardContent>
          <Typography variant="h4" className="report-title">
            Report an Incident
          </Typography>

          {/* Incident Type */}
          <TextField
            select
            label="Incident Type"
            fullWidth
            className="report-field"
            defaultValue=""
          >
            {incidentTypes.map((type) => (
              <MenuItem key={type.value} value={type.value}>
                {type.label}
              </MenuItem>
            ))}
          </TextField>

          {/* Location cu hartă */}
          <TextField
            label="Location"
            fullWidth
            className="report-field"
            value={location}
            onClick={() => setOpenMap(true)}
            InputProps={{
              readOnly: true,
              style: { cursor: "pointer" },
            }}
            placeholder="Click to select location on map"
          />

          {/* Dialog cu harta */}
          <Dialog
            open={openMap}
            onClose={() => setOpenMap(false)}
            maxWidth="md"
            fullWidth
          >
            <DialogTitle>Selectează locația în Timișoara</DialogTitle>
            <DialogContent>
              <div
                style={{ height: "500px", width: "100%", marginBottom: "16px" }}
              >
                <MapContainer
                  center={[45.7489, 21.2087]}
                  zoom={13}
                  style={{ height: "100%", width: "100%" }}
                >
                  <TileLayer
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
                  />
                  <Marker position={position} />
                  <MapClickHandler />
                </MapContainer>
              </div>

              <TextField
                label="Adresa selectată"
                fullWidth
                value={tempAddress}
                onChange={(e) => setTempAddress(e.target.value)}
                margin="normal"
              />

              <div style={{ display: "flex", gap: "8px", marginTop: "16px" }}>
                <Button onClick={() => setOpenMap(false)} variant="outlined">
                  Anulează
                </Button>
                <Button
                  onClick={handleConfirmLocation}
                  variant="contained"
                  color="primary"
                >
                  Confirmă locația
                </Button>
              </div>
            </DialogContent>
          </Dialog>

          {/* Vehicle Plate */}
          <TextField
            label="License Plate (optional)"
            fullWidth
            className="report-field"
          />

          {/* Upload Image */}
          <div className="upload-box">
            <input
              accept="image/*"
              type="file"
              id="fileUpload"
              hidden
              onChange={handleImageUpload}
            />
            <label htmlFor="fileUpload">
              <Button variant="outlined" component="span" fullWidth>
                Upload Image
              </Button>
            </label>

            {uploadedImage && (
              <img
                src={uploadedImage}
                alt="preview"
                className="image-preview"
              />
            )}
          </div>

          {/* Description */}
          <TextField
            label="Description"
            fullWidth
            multiline
            rows={4}
            className="report-field"
          />

          <Button variant="contained" color="primary" fullWidth>
            Submit Report
          </Button>
        </CardContent>
      </Card>
    </div>
  );
};

export default Report;
