import axios from "axios";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { useSelector } from "react-redux";

export default function CreateConcert() {
  let subClaim = useSelector((state) => state.user.claims.find((claim) => claim.type === "sub"));
  let [concertType, setConcertType] = useState("classic");
  let {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  let createClicked = async (data) => {
    if (subClaim) {
      let sub = subClaim.value;
      let query;
      switch (concertType) {
        case "classic":
          query = "api/classicConcert/create";

          data.vocalistVoice = parseInt(data.vocalistVoice);
          break;
        case "openAir":
          query = "api/openAir/create";
          break;
        case "party":
          query = "api/party/create";

          data.minAge = parseInt(data.minAge);
          break;
        default:
          throw new Error("invalid type in switch statement");
      }

      data.concertInfo.location.address.country = parseInt(
        data.concertInfo.location.address.country
      );
      let date = new Date(data.datetime);
      data.datetime = date.toISOString();

      let resp = await axios.post(query, data, { headers: { "X-CSRF": 1 } });
      //console.log(resp.status, resp.statusText);
      alert("Created!!!");
    }
  };
  let content;
  switch (concertType) {
    case "classic":
      content = (
        <>
          <div>
            <label htmlFor="cond">Conductor:</label>
            <input
              id="cond"
              {...register("conductor", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 32, message: "No more than 32 characters!" },
                pattern: {
                  value: /^[\w]+( ?[\w]+ ?)*[\w]+$/,
                  message:
                    "Only alphanumeric characters and underscore! You must separate words with spaces!",
                },
              })}
            />
            {errors.conductor && <span className="attention">{errors.conductor.message}</span>}
          </div>
          <div>
            <label htmlFor="voice">Voice:</label>
            <select id="voice" {...register("vocalistVoice")}>
              <option value="0">None</option>
              <option value="1">Bass</option>
              <option value="2">Bariton</option>
              <option value="3">Tenor</option>
              <option value="4">Counter tenor</option>
              <option value="5">Counter alto</option>
              <option value="6">Mezzo soprano</option>
              <option value="7">Soprano</option>
            </select>
          </div>
        </>
      );
      break;
    case "openAir":
      content = (
        <>
          <div>
            <label htmlFor="head">Headliner:</label>
            <input
              id="head"
              {...register("headliner", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 32, message: "No more than 32 characters!" },
                pattern: {
                  value: /^[\w]+( ?[\w]+ ?)*[\w]+$/,
                  message:
                    "Only alphanumeric characters and underscore! You must separate words with spaces!",
                },
              })}
            />
            {errors.headliner && <span className="attention">{errors.headliner.message}</span>}
          </div>
          <div>
            <label htmlFor="how">How to reach:</label>
            <input
              id="how"
              {...register("howToReach", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 64, message: "No more than 64 characters!" },
                pattern: {
                  value: /^[\w]+( ?[\w]+ ?)*[\w]+$/,
                  message:
                    "Only alphanumeric characters and underscore! You must separate words with spaces!",
                },
              })}
            />
            {errors.howToReach && <span className="attention">{errors.howToReach.message}</span>}
          </div>
        </>
      );
      break;
    case "party":
      content = (
        <div>
          <label htmlFor="minimumAge">Min age:</label>
          <input
            id="minimumAge"
            {...register("minAge", {
              pattern: { value: /^\d+$/, message: "Enter a valid integer number!" },
              min: { value: 1, message: "Must be >= 1!" },
              max: { value: 99, message: "Must be <= 99!" },
            })}
          />
          {errors.minAge && <span className="attention">{errors.minAge.message}</span>}
        </div>
      );
      break;
    default:
      throw new Error("invalid type for form");
  }

  return (
    <section>
      <h2>Create concert</h2>
      <div>
        <button disabled={concertType === "classic"} onClick={() => setConcertType("classic")}>
          Classic
        </button>
        <button disabled={concertType === "openAir"} onClick={() => setConcertType("openAir")}>
          OpenAir
        </button>
        <button disabled={concertType === "party"} onClick={() => setConcertType("party")}>
          Party
        </button>
      </div>
      <form onSubmit={handleSubmit(createClicked)} noValidate={true}>
        <section className="create">
          <h3>
            {concertType === "classic" ? "Classic" : concertType === "party" ? "Party" : "OpenAir"}
          </h3>
          <div>
            Required fields are marked with:
            <strong className="attention">*</strong>
          </div>
          <div>
            <label htmlFor="cname">
              <strong className="attention">*</strong>
              Name:
            </label>
            <input
              id="cname"
              {...register("name", {
                required: "Enter concert name, please!",
                minLength: { value: 4, message: "At least 4 characters!" },
                pattern: {
                  value: /^[\w\.]+( ?[\w\.]+ ?)*[\w\.]+$/,
                  message:
                    "Only alphanumeric characters and underscore and dots! You must separate words with spaces!",
                },

                maxLength: { value: 32, message: "No more than 32 characters!" },
              })}
            />
            {errors.name ? <span>{errors.name.message}</span> : null}
          </div>
          <div>
            <label htmlFor="long">
              <strong className="attention">*</strong>
              Longitude:
            </label>
            <input
              placeholder="54.55"
              id="long"
              {...register("longitude", {
                required: "Enter longitude, please!",
                maxLength: { value: 8, message: "No more than 8 characters!" },
                pattern: { value: /^\d+\.?\d+$/, message: "Only decimals and dot!" },
              })}
            />
            {errors.longitude ? <span>{errors.longitude.message}</span> : null}
          </div>
          <div>
            <label htmlFor="lat">
              <strong className="attention">*</strong>
              Latitude:
            </label>
            <input
              placeholder="54.55"
              id="lat"
              {...register("latitude", {
                required: "Enter latitude, please!",
                maxLength: { value: 8, message: "No more than 8 characters!" },
                pattern: { value: /^\d+\.?\d+$/, message: "Only decimals and dot!" },
              })}
            />
            {errors.latitude ? <span>{errors.latitude.message}</span> : null}
          </div>
          <div>
            <label htmlFor="datetime">
              <strong className="attention">*</strong>
              Date and Time:
            </label>
            <input
              id="datetime"
              type="datetime-local"
              {...register("datetime", {
                required: "Enter datetime, please",
                validate: {
                  futureDate(date) {
                    return new Date(date) > new Date() || "Date and time must be in the future!";
                  },
                },
              })}
            />
            {errors.datetime ? <span>{errors.datetime.message}</span> : null}
          </div>
          {content}
          <section className="create">
            <h3>Additional information</h3>
            <div>
              <label htmlFor="perf">Performer:</label>
              <input
                id="perf"
                {...register("concertInfo.performer", {
                  minLength: { value: 2, message: "At least 2 characters!" },
                  maxLength: { value: 32, message: "No more than 32 characters!" },
                  pattern: {
                    value: /^[\w]+( ?[\w]+ ?)*[\w]+$/,
                    message:
                      "Only alphanumeric characters and underscore! You must separate words with spaces!",
                  },
                })}
              />
              {errors.concertInfo?.performer && <span>{errors.concertInfo.performer.message}</span>}
            </div>
            <div>
              <label htmlFor="total">
                <strong className="attention">*</strong>
                Total tickets:
              </label>
              <input
                id="total"
                type="number"
                placeholder="1001"
                {...register("concertInfo.totalTickets", {
                  required: "Enter a number, please!",
                  min: { value: 1, message: "must be >= 1!" },
                  max: { value: 20000, message: "must be <= 20000!" },
                })}
              />
              {errors.concertInfo?.totalTickets && (
                <span>{errors.concertInfo.totalTickets.message}</span>
              )}
            </div>
          </section>
        </section>
        <section className="create">
          <h3>Address</h3>
          <div>
            <label htmlFor="ln">Location name:</label>
            <input
              id="ln"
              {...register("concertInfo.location.name", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 32, message: "No more than 32 characters!" },
                pattern: {
                  value: /^[\w]+( ?[\w]+ ?)*[\w]+$/,
                  message:
                    "Only alphabetical characters and underscore! You must separate words with spaces!",
                },
              })}
            />
            {errors.concertInfo?.location?.name && (
              <span>{errors.concertInfo.location.name.message}</span>
            )}
          </div>
          <div>
            <label htmlFor="descr">Description:</label>
            <input
              id="descr"
              {...register("concertInfo.location.address.description", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 256, message: "No more than 256 characters!" },
                pattern: {
                  value: /^[\w\.]+( ?[\w\.]+ ?)*[\w\.]+$/,
                  message:
                    "Only alphabetical characters and underscore and dots! You must separate words with spaces!",
                },
              })}
            />
            {errors.concertInfo?.location?.address?.description && (
              <span>{errors.concertInfo.location.address.description.message}</span>
            )}
          </div>
          <div>
            <label htmlFor="country">Country:</label>
            <select id="country" {...register("concertInfo.location.address.country")}>
              <option value={0}>None</option>
              <option value={1}>USA</option>
              <option value={2}>Russia</option>
              <option value={3}>China</option>
              <option value={4}>Italy</option>
              <option value={5}>Germany</option>
              <option value={6}>UK</option>
            </select>
          </div>
          <div>
            <label htmlFor="region">Region:</label>
            <input
              id="region"
              {...register("concertInfo.location.address.region", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 32, message: "No more than 32 characters!" },
                pattern: {
                  value: /^[\w\.]+( ?[\w\.]+ ?)*[\w\.]+$/,
                  message:
                    "Only alphabetical characters and underscore and dots! You must separate words with spaces!",
                },
              })}
            />
            {errors.concertInfo?.location?.address?.region && (
              <span>{errors.concertInfo.location.address.region.message}</span>
            )}
          </div>
          <div>
            <label htmlFor="city">City:</label>
            <input
              id="city"
              {...register("concertInfo.location.address.city", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 32, message: "No more than 32 characters!" },
                pattern: {
                  value: /^[\w\.]+( ?[\w\.]+ ?)*[\w\.]+$/,
                  message:
                    "Only alphabetical characters and underscore and dots! You must separate words with spaces!",
                },
              })}
            />
            {errors.concertInfo?.location?.address?.city && (
              <span>{errors.concertInfo.location.address.city.message}</span>
            )}
          </div>
          <div>
            <label htmlFor="street">Street:</label>
            <input
              id="street"
              {...register("concertInfo.location.address.street", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 32, message: "No more than 32 characters!" },
                pattern: {
                  value: /^[\w\.]+( ?[\w\.]+ ?)*[\w\.]+$/,
                  message:
                    "Only alphabetical characters and underscore and dots! You must separate words with spaces!",
                },
              })}
            />
            {errors.concertInfo?.location?.address?.street && (
              <span>{errors.concertInfo.location.address.street.message}</span>
            )}
          </div>
          <div>
            <label htmlFor="build">Building:</label>
            <input
              id="build"
              {...register("concertInfo.location.address.building", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 16, message: "No more than 16 characters!" },
                pattern: {
                  value: /^[\w\.]+( ?[\w\.]+ ?)*[\w\.]+$/,
                  message:
                    "Only alphabetical characters and underscore and dots! You must separate words with spaces!",
                },
              })}
            />
            {errors.concertInfo?.location?.address?.building && (
              <span>{errors.concertInfo.location.address.building.message}</span>
            )}
          </div>
          <div>
            <label htmlFor="floor">Floor:</label>
            <input
              id="floor"
              {...register("concertInfo.location.address.floor", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 16, message: "No more than 16 characters!" },
                pattern: {
                  value: /^[\w\.]+( ?[\w\.]+ ?)*[\w\.]+$/,
                  message:
                    "Only alphabetical characters and underscore and dots! You must separate words with spaces!",
                },
              })}
            />
            {errors.concertInfo?.location?.address?.floor && (
              <span>{errors.concertInfo.location.address.floor.message}</span>
            )}
          </div>
          <div>
            <label htmlFor="room">Room:</label>
            <input
              id="room"
              {...register("concertInfo.location.address.room", {
                minLength: { value: 2, message: "At least 2 characters!" },
                maxLength: { value: 16, message: "No more than 16 characters!" },
                pattern: {
                  value: /^[\w\.]+( ?[\w\.]+ ?)*[\w\.]+$/,
                  message:
                    "Only alphabetical characters and underscore and dots! You must separate words with spaces!",
                },
              })}
            />
            {errors.concertInfo?.location?.address?.room && (
              <span>{errors.concertInfo.location.address.room.message}</span>
            )}
          </div>
        </section>
        <div>
          <button type="submit">Create concert</button>
        </div>
      </form>
    </section>
  );
}
