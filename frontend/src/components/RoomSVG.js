import React from "react";
import styles from "../styles/roomsvg.module.css";
const RoomSVG = ({ onRoomClick }) => {
  const handleClick = (e) => {
    if (onRoomClick) {
      onRoomClick(e);
    }
  };

  return (
    <svg
      id="buildingPlanSvg"
      baseProfile="tiny"
      height="1300"
      stroke="#000000"
      strokeLinecap="round"
      strokeLinejoin="round"
      strokeWidth="2"
      version="1.2"
      viewBox="0 0 1870 1200"
      width="1870"
      xmlns="http://www.w3.org/2000/svg"
      className={styles.svgClass} // Add the className here
      style={{ backgroundColor: "black", marginTop: "-200px" }} // Set the background color to white and top margin to negative
    >
      <defs>
        <linearGradient id="roomGradient" x1="0%" y1="0%" x2="100%" y2="100%">
          <stop offset="0%" style={{ stopColor: "#d4e6f1", stopOpacity: 1 }} />{" "}
          {/* Light Blue */}
          <stop
            offset="100%"
            style={{ stopColor: "#3498db", stopOpacity: 1 }}
          />{" "}
          {/* Blue */}
        </linearGradient>
        <linearGradient
          id="roomGradientHover"
          x1="0%"
          y1="0%"
          x2="100%"
          y2="100%"
        >
          <stop offset="0%" style={{ stopColor: "#aed6f1", stopOpacity: 1 }} />{" "}
          {/* Lighter Blue */}
          <stop
            offset="100%"
            style={{ stopColor: "#5dade2", stopOpacity: 1 }}
          />{" "}
          {/* Medium Blue */}
        </linearGradient>
        <linearGradient
          id="roomGradientSelected"
          x1="0%"
          y1="0%"
          x2="100%"
          y2="100%"
        >
          <stop offset="0%" style={{ stopColor: "#f7dc6f", stopOpacity: 1 }} />{" "}
          {/* Yellow */}
          <stop
            offset="100%"
            style={{ stopColor: "#f1c40f", stopOpacity: 1 }}
          />{" "}
          {/* Dark Yellow */}
        </linearGradient>
      </defs>

      <g>
        <path
          className="room"
          d="m 276.70779,549.06482 141.83449,1.7403 0.87015,-99.19713 -143.57479,0.87015 z"
          id="21"
          onClick={handleClick}
        >
          <title>room21</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 105.28818,770.95314 h 291.50034 l 0.87015,298.46156 H 264.52568 l -159.2375,81.7941 z"
          id="1"
          onClick={handleClick}
        >
          <title>room1</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 408.10048,769.21284 v 299.33166 l 121.82103,0.8702 0.87015,-299.33171 z"
          id="2"
          onClick={handleClick}
        >
          <title>room2</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="M 584.74098,1023.2967 H 693.50976 V 919.74883 l 25.23436,-1.7403 -0.87015,-147.05539 -102.67773,0.87015 v 40.89706 h -30.45526 z"
          id="3"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room3</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 729.18592,770.08299 v 147.05539 l 125.30164,0.87015 -0.87015,-147.92554 z"
          id="4"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room4</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 864.92937,770.08299 v 298.46151 l 298.46153,0.8702 0.8702,-299.33171 z"
          id="5"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room5</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1175.573,770.08299 v 298.46151 l 280.1884,-45.2478 V 770.08299 Z"
          id="6"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room6</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1467.0734,770.95314 -0.8702,154.88675 79.1837,0.87015 V 770.08299 Z"
          id="7"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room07</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1556.6988,770.08299 v 154.88675 h 64.3912 l 0.8701,-154.88675 z"
          id="8"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room08</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1467.0734,937.15184 -1.7403,168.80916 297.5913,-50.4687 -0.8701,-174.90023 -129.6524,-0.87015 v 56.55977 z"
          id="9"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room09</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1691.5721,550.80512 h 71.3523 V 335.00785 l -40.897,-24.3642 h -31.3254 z"
          id="10"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room10</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1680.2602,549.93497 -86.1449,-0.87015 V 310.64365 l 87.015,0.87015 z"
          id="11"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room11</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1503.6197,549.06482 77.4433,1.7403 1.7403,-238.42117 -79.1836,-1.7403 z"
          id="12"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room12</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1411.3837,549.93497 82.6643,0.87015 -1.7403,-239.29132 -80.924,-0.87015 z"
          id="13"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room13</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1307.8359,549.93497 h 93.9762 V 311.5138 l -93.9762,-0.87015 z"
          id="14"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room14</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1173.224,549.38718 h 123.2271 l 1.2836,-444.13076 H 1173.224 Z"
          id="15"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room15</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 1162.9551,548.10357 -105.2564,1.28361 V 310.63481 h 103.9728 z"
          id="16"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room16</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="M 1048.7134,549.38718 H 874.14176 l 1.28361,-446.69799 286.24613,1.28362 v 193.82585 l -114.2417,1.28362 z"
          id="17"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room17</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 758.61642,311.91842 -2.56723,238.75238 109.10726,-1.28362 1.28362,-240.03598 z"
          id="18"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room18</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="M 576.3431,548.10357 V 103.97281 l 287.52974,2.56723 V 301.64951 L 747.06388,300.36589 V 550.6708 Z"
          id="19"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room19</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 430.011,103.97281 v 445.41437 h 134.77956 l 1.28362,-445.41437 z"
          id="20"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room20</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 417.17485,441.56353 -141.19764,1.28362 V 342.72518 l 139.91402,1.28362 z"
          id="22"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room22</title>
        </path>
        <path
          className={`room ${styles.roomColor}`}
          d="m 419.74208,332.45626 -1.28362,-227.19984 h -94.9875 l 3.85084,137.3468 h -50.06098 l -1.28361,88.56943 z"
          id="23"
          onClick={(e) => onRoomClick(e)}
        >
          <title>room23</title>
        </path>
      </g>
    </svg>
  );
};

export default RoomSVG;
